using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JVM.ClassDescription;
using JVM.Runner;

namespace JVM {
    class Program {
        public static string MAIN_NAME = "main";
        public static string MAIN_SIGNATURE = "([Ljava/lang/String;)V";

        private static List<Instruction> ParseInstuctions(byte[] code) {
            List<Instruction> res = new List<Instruction>();
            int pos = 0;

            while (pos < code.Length) {
                OpCode opCode = null;
                OpCodes.VALUE_TO_OPCODE_MAP.TryGetValue(code[pos++], out opCode);

                if (opCode == null) {
                    throw new Exception("Unknown opcode is used, terminating!");
                }

                byte[] operand = new byte[opCode.OperandSize];
                for (int i = 0; i < opCode.OperandSize; ++i) {
                    operand[i] = code[pos++];
                }
                operand = Utils.ReverseIfEndian(operand);

                res.Add(new Instruction { OpCode = opCode, Operand = operand });
            }
            return res;
        }

        private static void RunMethod(MethodInfo m, IEnumerable<int> args = null) {
            var codeAttr = m.AttributeParsers["Code"] as CodeAttributeParser;
            if (codeAttr == null) {
                throw new Exception("Code attribute is not found!");
            }

            List<Instruction> instructions = ParseInstuctions(codeAttr.Code);
            IDictionary<int, Instruction> offsetMap = new Dictionary<int, Instruction>();
            int offset = 0;
            foreach(Instruction instr in instructions) {
                offsetMap.Add(offset, instr);
                offset += 1 + (int)instr.OpCode.OperandSize;
            }

            Frame frame = new Frame(codeAttr.MaxStack, codeAttr.MaxLocals);
            if (args != null) {
                foreach (int u in args) {
                    // TODO: Check that it is pushing in the right order
                    frame.Locals.Add(u);
                }
            }

            while(!frame.IsReturned) {
                Instruction instr = offsetMap[frame.IP];
                InstructionExecutor.EXECUTORS_MAP[instr.OpCode.Name](instr.Operand, frame, cFile);
            }
        }

        public static void Main(string[] args) {
            byte[] data = File.ReadAllBytes(@"..\..\NOD.class");
            ClassFile cFile = ClassFile.ParseClassFile(data);

            // TODO: check cFile.AccessFlags

            MethodInfo methodMain = null;
            foreach(var m in cFile.Methods) {
                if (m.AccessFlags != (short)(MethodAccessFlags.ACC_PUBLIC | MethodAccessFlags.ACC_STATIC)) {
                    continue;
                }
                var mName = (CONSTANT_Utf8_info)cFile.GetConstant(m.NameIndex).Info;
                if (!mName.String.Equals(MAIN_NAME)) {
                    continue;
                }

                var mSignature = (CONSTANT_Utf8_info)cFile.GetConstant(m.DescriptorIndex).Info;
                if (!mSignature.String.Equals(MAIN_SIGNATURE)) {
                    continue;
                }

                methodMain = m;
                break;
            }

            if (methodMain == null) {
                throw new Exception("Main method is not found!");
            }
            RunMethod(methodMain, null);
        }
    }
}
