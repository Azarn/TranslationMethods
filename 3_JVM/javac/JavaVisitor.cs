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
        BOOLEAN,
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

        public static Instruction GenerateGoto(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.@goto],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateTwoIntLT(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.if_icmplt],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateTwoIntLE(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.if_icmple],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateTwoIntGT(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.if_icmpgt],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateTwoIntGE(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.if_icmpge],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateTwoIntEqual(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.if_icmpeq],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateTwoIntNotEqual(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.if_icmpne],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateIntGT(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ifgt],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateIntGE(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ifge],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateIntLT(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iflt],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateIntLE(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ifle],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateIntEqual(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ifeq],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction GenerateIntNotEqual(short jumpOffset) {
            return new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ifne],
                Operand = BitConverter.GetBytes(jumpOffset)
            };
        }

        public static Instruction[] GenerateBooleanLT(TYPE type) {
            var res = new List<Instruction>();

            switch (type) {
                case TYPE.INT:
                    res.Add(GenerateTwoIntLT(7));
                    break;
                case TYPE.DOUBLE:
                    res.Add(new Instruction {
                        OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dcmpg]
                    });
                    res.Add(GenerateIntGT(7));
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot create compare `lower than` for type {0}", type));
            }

            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0]
            });
            res.Add(GenerateGoto(4));
            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            });

            return res.ToArray();
        }

        public static Instruction[] GenerateBooleanLE(TYPE type) {
            var res = new List<Instruction>();

            switch (type) {
                case TYPE.INT:
                    res.Add(GenerateTwoIntLE(7));
                    break;
                case TYPE.DOUBLE:
                    res.Add(new Instruction {
                        OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dcmpg]
                    });
                    res.Add(GenerateIntGE(7));
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot create compare `lower than` for type {0}", type));
            }

            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0]
            });
            res.Add(GenerateGoto(4));
            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            });

            return res.ToArray();
        }

        public static Instruction[] GenerateBooleanGT(TYPE type) {
            var res = new List<Instruction>();

            switch (type) {
                case TYPE.INT:
                    res.Add(GenerateTwoIntGT(7));
                    break;
                case TYPE.DOUBLE:
                    res.Add(new Instruction {
                        OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dcmpg]
                    });
                    res.Add(GenerateIntLT(7));
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot create compare `lower than` for type {0}", type));
            }

            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0]
            });
            res.Add(GenerateGoto(4));
            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            });

            return res.ToArray();
        }

        public static Instruction[] GenerateBooleanGE(TYPE type) {
            var res = new List<Instruction>();

            switch (type) {
                case TYPE.INT:
                    res.Add(GenerateTwoIntGE(7));
                    break;
                case TYPE.DOUBLE:
                    res.Add(new Instruction {
                        OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dcmpg]
                    });
                    res.Add(GenerateIntLE(7));
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot create compare `lower than` for type {0}", type));
            }

            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0]
            });
            res.Add(GenerateGoto(4));
            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            });

            return res.ToArray();
        }

        public static Instruction[] GenerateBooleanEqual(TYPE type) {
            var res = new List<Instruction>();

            switch (type) {
                case TYPE.BOOLEAN:
                case TYPE.INT:
                    res.Add(GenerateTwoIntEqual(7));
                    break;
                case TYPE.DOUBLE:
                    res.Add(new Instruction {
                        OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dcmpg]
                    });
                    res.Add(GenerateIntEqual(7));
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot create compare `lower than` for type {0}", type));
            }

            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0]
            });
            res.Add(GenerateGoto(4));
            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            });

            return res.ToArray();
        }

        public static Instruction[] GenerateBooleanNotEqual(TYPE type) {
            var res = new List<Instruction>();

            switch (type) {
                case TYPE.BOOLEAN:
                case TYPE.INT:
                    res.Add(GenerateTwoIntNotEqual(7));
                    break;
                case TYPE.DOUBLE:
                    res.Add(new Instruction {
                        OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dcmpg]
                    });
                    res.Add(GenerateIntNotEqual(7));
                    break;
                // TODO: Implement FLOAT
                default:
                    throw new Exception(string.Format("Cannot create compare `lower than` for type {0}", type));
            }

            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0]
            });
            res.Add(GenerateGoto(4));
            res.Add(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            });

            return res.ToArray();
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
            // TODO: Detect FLOAT type
            var expr = new ExpressionValue();
            var doubleVal = context.number.DOUBLE_NUM();
            var intVal = context.number.INTEGER_NUM();
            var instruction = new Instruction();

            if (intVal != null) {
                expr.type = TYPE.INT;
                int value = int.Parse(intVal.GetText());
                switch (value) {
                    case -1:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_m1];
                        break;
                    case 0:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0];
                        break;
                    case 1:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1];
                        break;
                    case 2:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_2];
                        break;
                    case 3:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_3];
                        break;
                    case 4:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_4];
                        break;
                    case 5:
                        instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_5];
                        break;
                    default:
                        if (value <= sbyte.MaxValue) {
                            instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.bipush];
                            instruction.Operand = new byte[1] { (byte)value };
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
                        break;
                }
            } else if (doubleVal != null) {
                expr.type = TYPE.DOUBLE;
                double value = double.Parse(doubleVal.GetText());
                if (value == 0.0) {
                    instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dconst_0];
                } else if (value == 1.0) {
                    instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.dconst_1];
                } else {
                    var data = (ulong)BitConverter.DoubleToInt64Bits(value);
                    byte[] highbytes = BitConverter.GetBytes((uint)(data >> 32));
                    byte[] lowbytes = BitConverter.GetBytes((uint)data);
                    var index = _stackFrame.GetOrAddConstant8(highbytes, lowbytes, ConstantPoolTag.CONSTANT_Double);
                    instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ldc2_w];
                    instruction.Operand = BitConverter.GetBytes(index);
                }
            } else {
                throw new Exception("Unknown number value!");
            }

            AddInstruction(instruction, ref expr);
            return expr;
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

        public override ExpressionValue VisitBoolean([NotNull] JavaParser.BooleanContext context) {
            var expr = new ExpressionValue { type = TYPE.BOOLEAN };
            var instruction = new Instruction();
            if (context.boolean_val().TRUE() != null) {
                instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1];
            } else {
                instruction.OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_0];
            }
            AddInstruction(instruction, ref expr);
            return expr;
        }

        public override ExpressionValue VisitLogicalNot([NotNull] JavaParser.LogicalNotContext context) {
            var expr = context.expression_value().Accept(this);
            if (expr.type != TYPE.BOOLEAN) {
                throw new Exception("Cannot cast expression to boolean!");
            }

            AddInstruction(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.iconst_1]
            }, ref expr);

            AddInstruction(new Instruction {
                OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ixor]
            }, ref expr);
            
            return expr;
        }

        public override ExpressionValue VisitUnaryPlusMinus([NotNull] JavaParser.UnaryPlusMinusContext context) {
            var expr = context.expression_value().Accept(this);
            if (context.SUB() != null) {
                AddInstruction(new Instruction {
                    OpCode = OpCodes.NAME_TO_OPCODE_MAP[OpCodeName.ineg]
                }, ref expr);
            }
            return expr;
        }

        public override ExpressionValue VisitMulDivRem([NotNull] JavaParser.MulDivRemContext context) {
            // TODO: Check casts
            var expr = context.first.Accept(this);
            var second_expr = context.second.Accept(this);
            if (expr.type != second_expr.type) {
                throw new Exception("Incorrect types!");
            }
            expr.instructions.AddRange(second_expr.instructions);

            switch (context.op.Text) {
                case "*":
                    AddInstruction(InstructionGenerator.GenerateMultiplication(expr.type), ref expr);
                    break;
                case "/":
                    AddInstruction(InstructionGenerator.GenerateDivision(expr.type), ref expr);
                    break;
                case "%":
                    AddInstruction(InstructionGenerator.GenerateReminder(expr.type), ref expr);
                    break;
            }
            return expr;
        }

        public override ExpressionValue VisitAddSub([NotNull] JavaParser.AddSubContext context) {
            // TODO: Check casts
            var expr = context.first.Accept(this);
            var second_expr = context.second.Accept(this);
            if (expr.type != second_expr.type) {
                throw new Exception("Incorrect types!");
            }
            expr.instructions.AddRange(second_expr.instructions);

            switch (context.op.Text) {
                case "+":
                    AddInstruction(InstructionGenerator.GenerateAddition(expr.type), ref expr);
                    break;
                case "-":
                    AddInstruction(InstructionGenerator.GenerateSubtraction(expr.type), ref expr);
                    break;
            }
            return expr;
        }

        public override ExpressionValue VisitLtLeGtGe([NotNull] JavaParser.LtLeGtGeContext context) {
            // TODO: Check casts
            var expr = context.first.Accept(this);
            var second_expr = context.second.Accept(this);
            if (expr.type != second_expr.type) {
                throw new Exception("Incorrect types!");
            }
            expr.instructions.AddRange(second_expr.instructions);

            switch (context.op.Text) {
                case ">":
                    foreach (var instruction in InstructionGenerator.GenerateBooleanGT(expr.type)) {
                        AddInstruction(instruction, ref expr);
                    }
                    break;
                case ">=":
                    foreach (var instruction in InstructionGenerator.GenerateBooleanGE(expr.type)) {
                        AddInstruction(instruction, ref expr);
                    }
                    break;
                case "<":
                    foreach(var instruction in InstructionGenerator.GenerateBooleanLT(expr.type)) {
                        AddInstruction(instruction, ref expr);
                    }
                    break;
                case "<=":
                    foreach (var instruction in InstructionGenerator.GenerateBooleanLE(expr.type)) {
                        AddInstruction(instruction, ref expr);
                    }
                    break;
            }

            expr.type = TYPE.BOOLEAN;
            return expr;
        }

        public override ExpressionValue VisitEqualNotEqual([NotNull] JavaParser.EqualNotEqualContext context) {
            // TODO: Check casts
            var expr = context.first.Accept(this);
            var second_expr = context.second.Accept(this);
            if (expr.type != second_expr.type) {
                throw new Exception("Incorrect types!");
            }
            expr.instructions.AddRange(second_expr.instructions);

            switch (context.op.Text) {
                case "==":
                    foreach (var instruction in InstructionGenerator.GenerateBooleanEqual(expr.type)) {
                        AddInstruction(instruction, ref expr);
                    }
                    break;
                case "!=":
                    foreach (var instruction in InstructionGenerator.GenerateBooleanNotEqual(expr.type)) {
                        AddInstruction(instruction, ref expr);
                    }
                    break;
            }

            expr.type = TYPE.BOOLEAN;
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
