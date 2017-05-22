using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using JVM.Runner;
using JVM.ClassDescription;

namespace javac {
    public enum TYPE {
        INT,
        DOUBLE,
        FLOAT,
        VOID
    }

    class ExpressionValue {
        public List<Instruction> instructions = new List<Instruction>();
        public TYPE type = TYPE.VOID;
    }

    public struct Variable {
        public TYPE type;
        public byte index;

        public override string ToString() {
            return string.Format("[{0}] {1}", index, type);
        }
    }

    class StackFrame {
        private int _currentStackSize;
        public int maxStackSize;
        public int maxLocalsSize;
        public int CurrentStackSize {
            get {
                return _currentStackSize;
            }
            set {
                _currentStackSize = value;
                maxStackSize = Math.Max(maxStackSize, value);
            }
        }
        public Dictionary<string, Variable> localsMap = new Dictionary<string, Variable>();
        public List<Instruction> instructions = new List<Instruction>();
        public short constantPoolMaxIndex;
        public Dictionary<short, short> constantPoolIndexFix = new Dictionary<short, short>();
        public List<ConstantPoolDescription> constantPool = new List<ConstantPoolDescription>();

        public Variable AddVariable(string name, TYPE type) {
            if (localsMap.ContainsKey(name)) {
                throw new Exception("Variable already declared!");
            }

            if (type == TYPE.VOID) {
                throw new Exception("Type `void` is not allowed for variable!");
            }

            var variable = new Variable {
                type = type,
                index = (byte)localsMap.Count
            };
            localsMap.Add(name, variable);
            maxLocalsSize = Math.Max(maxLocalsSize, localsMap.Count);
            return variable;
        }

        public Variable GetVariable(string name) {
            Variable variable;
            if (!localsMap.TryGetValue(name, out variable)) {
                throw new Exception(string.Format("Variable {0} is not declared!", name));
            }
            return variable;
        }

        public StackFrame CreateCopy() {
            return new StackFrame {
                maxStackSize = maxStackSize,
                maxLocalsSize = maxLocalsSize,
                _currentStackSize = _currentStackSize,
                localsMap = new Dictionary<string, Variable>(localsMap),
                constantPool = constantPool
            };
        }

        public void JoinStacksWithoutLocals(StackFrame other) {
            maxLocalsSize = Math.Max(maxLocalsSize, other.maxLocalsSize);
            maxStackSize = Math.Max(maxStackSize, other.maxStackSize);
            instructions.AddRange(other.instructions);
        }

        public short GetOrAddConstant4(byte[] value, ConstantPoolTag tag) {
            for(short i = 0; i < constantPool.Count; ++i) {
                if (constantPool[i].Tag == tag && ((CONSTANT_B4_info)constantPool[i].Info).Bytes == value) {
                    return constantPoolIndexFix[i];
                }
            }
            short res = (short)constantPool.Count;
            constantPool.Add(new ConstantPoolDescription(tag, new CONSTANT_B4_info(value)));
            constantPoolMaxIndex += 1;
            constantPoolIndexFix.Add(res, constantPoolMaxIndex);
            return res;
        }

        public short GetOrAddConstant8(byte[] highvalue, byte[] lowvalue, ConstantPoolTag tag) {
            for (short i = 0; i < constantPool.Count; ++i) {
                var cnst = (CONSTANT_B8_info)constantPool[i].Info;
                if (constantPool[i].Tag == tag && cnst.HighBytes == highvalue && cnst.LowBytes == lowvalue) {
                    return constantPoolIndexFix[i];
                }
            }
            short res = (short)constantPool.Count;
            constantPool.Add(new ConstantPoolDescription(tag, new CONSTANT_B8_info(highvalue, lowvalue)));
            constantPoolMaxIndex += 2;
            constantPoolIndexFix.Add(res, constantPoolMaxIndex);
            return res;
        }

        public void AddInstructionAndRecalcSize(Instruction instruction) {
            instructions.Add(instruction);
            CurrentStackSize += instruction.OpCode.StackChange;
        }
    }

    public static class InstructionGenerator {
        public static Instruction GenerateVariableAssignment(Variable variable, TYPE expression_type) {
            if (expression_type != variable.type) {
                throw new Exception(string.Format("Cannot cast {0} to {1}", expression_type, variable.type));
            }
            var instruction = new Instruction();
            switch (variable.type) {
                case TYPE.INT:
                    switch (variable.index) {
                        case 0:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.istore_0];
                            break;
                        case 1:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.istore_1];
                            break;
                        case 2:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.istore_2];
                            break;
                        case 3:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.istore_3];
                            break;
                        default:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.istore];
                            instruction.Operand = new byte[1] { variable.index };
                            break;
                    }
                    break;
                case TYPE.DOUBLE:
                    switch (variable.index) {
                        case 0:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dstore_0];
                            break;
                        case 1:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dstore_1];
                            break;
                        case 2:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dstore_2];
                            break;
                        case 3:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dstore_3];
                            break;
                        default:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dstore];
                            instruction.Operand = new byte[1] { variable.index };
                            break;
                    }
                    break;
                    // TODO: Implement FLOAT type
            }
            return instruction;
        }

        public static Instruction GenerateAddition(TYPE type) {
            OpCodeName opName;
            switch (type) {
                case TYPE.INT:
                    opName = OpCodeName.iadd;
                    break;
                case TYPE.DOUBLE:
                    opName = OpCodeName.dadd;
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot add type {0}", type));
            }
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[opName]
            };
        }

        public static Instruction GenerateSubtraction(TYPE type) {
            OpCodeName opName;
            switch (type) {
                case TYPE.INT:
                    opName = OpCodeName.isub;
                    break;
                case TYPE.DOUBLE:
                    opName = OpCodeName.dsub;
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot subtract type {0}", type));
            }
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[opName]
            };
        }

        public static Instruction GenerateMultiplication(TYPE type) {
            OpCodeName opName;
            switch (type) {
                case TYPE.INT:
                    opName = OpCodeName.imul;
                    break;
                case TYPE.DOUBLE:
                    opName = OpCodeName.dmul;
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot multiplicate type {0}", type));
            }
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[opName]
            };
        }

        public static Instruction GenerateDivision(TYPE type) {
            OpCodeName opName;
            switch (type) {
                case TYPE.INT:
                    opName = OpCodeName.idiv;
                    break;
                case TYPE.DOUBLE:
                    opName = OpCodeName.ddiv;
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot divide type {0}", type));
            }
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[opName]
            };
        }

        public static Instruction GenerateReminder(TYPE type) {
            OpCodeName opName;
            switch (type) {
                case TYPE.INT:
                    opName = OpCodeName.irem;
                    break;
                case TYPE.DOUBLE:
                    opName = OpCodeName.drem;
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot get reminder for type {0}", type));
            }
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[opName]
            };
        }

        public static Instruction GenerateVariableLoad(Variable variable) {
            var instruction = new Instruction();
            switch (variable.type) {
                case TYPE.INT:
                    switch (variable.index) {
                        case 0:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iload_0];
                            break;
                        case 1:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iload_1];
                            break;
                        case 2:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iload_2];
                            break;
                        case 3:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iload_3];
                            break;
                        default:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iload];
                            instruction.Operand = new byte[1] { variable.index };
                            break;
                    }
                    break;
                case TYPE.DOUBLE:
                    switch (variable.index) {
                        case 0:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dload_0];
                            break;
                        case 1:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dload_1];
                            break;
                        case 2:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dload_2];
                            break;
                        case 3:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dload_3];
                            break;
                        default:
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dload];
                            instruction.Operand = new byte[1] { variable.index };
                            break;
                    }
                    break;
                    // TODO: Implement FLOAT type
            }
            return instruction;
        }

        /*public static Instruction GenerateSystemPrint(TYPE type) {
            return null;
        }*/

        public static Instruction GenerateSystemPrintln(TYPE type) {
            OpCodeName opName;
            switch(type) {
                case TYPE.INT:
                    opName = OpCodeName.da_oni_nikto_epta_i;
                    break;
                case TYPE.DOUBLE:
                    opName = OpCodeName.da_oni_nikto_epta_d;
                    break;
                case TYPE.FLOAT:
                    opName = OpCodeName.da_oni_nikto_epta_f;
                    break;
                default:
                    throw new Exception(string.Format("Incorrect type {0}", type));
            }
            return new Instruction { OpCode = OpCodes.NAME_TO_OPCODE_MAP[opName] };
        }
    }

    class VariableTypeVisitor : JavaBaseVisitor<TYPE> {
        public override TYPE VisitVar_type([NotNull] JavaParser.Var_typeContext context) {
            switch (context.type.Text) {
                case "int":
                    return TYPE.INT;
                case "double":
                    return TYPE.DOUBLE;
                case "float":
                    return TYPE.FLOAT;
            }
            throw new Exception("Invalid variable type!");
        }
    }

    class ExpressionValueVisitor : JavaBaseVisitor<ExpressionValue> {
        private StackFrame _stackFrame;
        public ExpressionValueVisitor(StackFrame stackFrame) {
            _stackFrame = stackFrame;
        }

        private void AddInstruction(Instruction instruction, ref ExpressionValue expr) {
            expr.instructions.Add(instruction);
            _stackFrame.CurrentStackSize += instruction.OpCode.StackChange;
        }
        
        /*private void AddRangeInstructions(IEnumerable<Instruction> instructions, ref ExpressionValue expr) {
            foreach (var instr in instructions) {
                AddInstruction(instr, ref expr);
            }
        }*/

        public override ExpressionValue VisitVarName([NotNull] JavaParser.VarNameContext context) {
            var variable = _stackFrame.GetVariable(context.name.Text);
            var res = new ExpressionValue { type = variable.type };
            var instruction = InstructionGenerator.GenerateVariableLoad(variable);
            AddInstruction(instruction, ref res);
            return res;
        }

        public override ExpressionValue VisitNumber([NotNull] JavaParser.NumberContext context) {
            // TODO: Detect not only INT type
            var expr = new ExpressionValue { type = TYPE.INT };
            int value = int.Parse(context.number.Text);
            Instruction instruction = new Instruction();
            if (value <= sbyte.MaxValue) {
                instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.bipush];
                instruction.Operand = new byte[1] { (byte) value };
            } else if (value <= short.MaxValue) {
                instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.sipush];
                instruction.Operand = BitConverter.GetBytes((short)value);
            } else {
                var index = _stackFrame.GetOrAddConstant4(BitConverter.GetBytes(value), ConstantPoolTag.CONSTANT_Integer);
                if (index <= byte.MaxValue) {
                    instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ldc];
                    instruction.Operand = new byte[1] { (byte)index };
                } else {
                    instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ldc_w];
                    instruction.Operand = BitConverter.GetBytes(index);
                }
            }

            AddInstruction(instruction, ref expr);
            return expr;
        }

        public override ExpressionValue VisitExpressionChain([NotNull] JavaParser.ExpressionChainContext context) {
            var exprs = context.expression_value();
            var first_expr = exprs[0].Accept(this);
            var second_expr = exprs[1].Accept(this);
            var op = context.op()[0];
            // TODO: Check casts
            first_expr.instructions.AddRange(second_expr.instructions);
            switch (op.GetText()) {
                // TODO: Implement others
                case "+":
                    AddInstruction(InstructionGenerator.GenerateAddition(first_expr.type), ref first_expr);
                    break;
                case "-":
                    AddInstruction(InstructionGenerator.GenerateSubtraction(first_expr.type), ref first_expr);
                    break;
            }
            return first_expr;
        }

        public override ExpressionValue VisitAssignment([NotNull] JavaParser.AssignmentContext context) {
            // TODO: Check implicit(?) casts
            var variable = _stackFrame.GetVariable(context.name.Text);
            var expr = new ExpressionValue { type = variable.type };
            var expr_assignment = context.expression_value().Accept(this);
            var lop = context.lop().GetText();
            var instr_assign = InstructionGenerator.GenerateVariableAssignment(variable, expr.type);

            if (lop != "=") {
                AddInstruction(InstructionGenerator.GenerateVariableLoad(variable), ref expr);
            }
            expr.instructions.AddRange(expr_assignment.instructions);

            switch (lop) {
                case "+=":
                    AddInstruction(InstructionGenerator.GenerateAddition(expr.type), ref expr);
                    break;
                case "-=":
                    AddInstruction(InstructionGenerator.GenerateSubtraction(expr.type), ref expr);
                    break;
                case "*=":
                    AddInstruction(InstructionGenerator.GenerateMultiplication(expr.type), ref expr);
                    break;
                case "/=":
                    AddInstruction(InstructionGenerator.GenerateDivision(expr.type), ref expr);
                    break;
                case "%=":
                    AddInstruction(InstructionGenerator.GenerateReminder(expr.type), ref expr);
                    break;
            }

            AddInstruction(instr_assign, ref expr);
            return expr;
        }

        public override ExpressionValue VisitParenExpr([NotNull] JavaParser.ParenExprContext context) {
            return context.expression_value().Accept(this);
        }

        public override ExpressionValue VisitPureExpr([NotNull] JavaParser.PureExprContext context) {
            return context.expression().assignment().Accept(this);
        }
    }

    class JavaVisitor : JavaBaseVisitor<StackFrame> {
        private StackFrame _stackFrame;

        public StackFrame StackFrame => _stackFrame;

        public JavaVisitor(StackFrame stackFrame) {
            _stackFrame = stackFrame;
        }

        public override StackFrame VisitMain_args([NotNull] JavaParser.Main_argsContext context) {
            _stackFrame.AddVariable(context.argName.Text, TYPE.INT);        // actually it is a string
            return base.VisitMain_args(context);
        }

        public override StackFrame VisitNewBlock([NotNull] JavaParser.NewBlockContext context) {
            var visitor = new JavaVisitor(_stackFrame.CreateCopy());
            foreach(var statement in context.statement()) {
                statement.Accept(visitor);
            }

            _stackFrame.JoinStacksWithoutLocals(visitor.StackFrame);
            return _stackFrame;
        }

        public override StackFrame VisitVar_decl([NotNull] JavaParser.Var_declContext context) {
            var type = context.var_type().Accept(new VariableTypeVisitor());
            var variable = _stackFrame.AddVariable(context.name.Text, type);
            if (context.expr != null) {
                var expr = context.expression_value().Accept(new ExpressionValueVisitor(_stackFrame));
                var instruction = InstructionGenerator.GenerateVariableAssignment(variable, expr.type);
                _stackFrame.instructions.AddRange(expr.instructions);
                _stackFrame.AddInstructionAndRecalcSize(instruction);
            }
            return _stackFrame;
        }

        public override StackFrame VisitExpressionStatement([NotNull] JavaParser.ExpressionStatementContext context) {
            var expr = context.expression().assignment().Accept(new ExpressionValueVisitor(_stackFrame));
            _stackFrame.instructions.AddRange(expr.instructions);
            return _stackFrame;
        }

        public override StackFrame VisitPrint_call([NotNull] JavaParser.Print_callContext context) {
            var expr = context.expression_value().Accept(new ExpressionValueVisitor(_stackFrame));
            _stackFrame.instructions.AddRange(expr.instructions);
            if (context.PRINTLN() != null) {
                _stackFrame.AddInstructionAndRecalcSize(InstructionGenerator.GenerateSystemPrintln(expr.type));
            } else {

            }
            return _stackFrame;
        }
    }
}
