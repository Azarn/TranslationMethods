using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.Runner {
    public static class InstructionExecutor {
        public delegate void ExecuteAction(Instruction instruction, Frame frame);

        public static IDictionary<OpCodeName, ExecuteAction> EXECUTORS_MAP = new Dictionary<OpCodeName, ExecuteAction>() {
            { OpCodeName.bipush, bipush },
            { OpCodeName.d2f, d2f },
            { OpCodeName.d2i, d2i },
            { OpCodeName.d2l, d2l },
            { OpCodeName.@goto, @goto },
            { OpCodeName.iadd, iadd },
            { OpCodeName.iconst_m1, iconst_m1 },
            { OpCodeName.iconst_0, iconst_0 },
            { OpCodeName.iconst_1, iconst_1 },
            { OpCodeName.iconst_2, iconst_2 },
            { OpCodeName.iconst_3, iconst_3 },
            { OpCodeName.iconst_4, iconst_4 },
            { OpCodeName.iconst_5, iconst_5 },
            { OpCodeName.ifeq, ifeq },
            { OpCodeName.if_icmple, if_icmple },
            { OpCodeName.iload, iload },
            { OpCodeName.iload_0, iload_0 },
            { OpCodeName.iload_1, iload_1 },
            { OpCodeName.iload_2, iload_2 },
            { OpCodeName.iload_3, iload_3 },
            { OpCodeName.irem, irem },
            { OpCodeName.istore, istore },
            { OpCodeName.istore_0, istore_0 },
            { OpCodeName.istore_1, istore_1 },
            { OpCodeName.istore_2, istore_2 },
            { OpCodeName.istore_3, istore_3 },
            { OpCodeName.nop, nop },
            { OpCodeName.@return, @return },
            { OpCodeName.sipush, sipush },

            { OpCodeName.da_oni_nikto_epta, da_oni_nikto_epta }
        };

        private static void bipush(Instruction instruction, Frame frame) {
            frame.Stack.Push(instruction.Operand[0]);
            nop(instruction, frame);
        }

        private static void d2f(Instruction instruction, Frame frame) {
            frame.PutFloatToStack((float)frame.GetDoubleFromStack());
            nop(instruction, frame);
        }

        private static void d2i(Instruction instruction, Frame frame) {
            frame.Stack.Push((int)frame.GetDoubleFromStack());
            nop(instruction, frame);
        }

        private static void d2l(Instruction instruction, Frame frame) {
            frame.PutLongToStack((long)frame.GetDoubleFromStack());
            nop(instruction, frame);
        }

        private static void @goto(Instruction instruction, Frame frame) {
            frame.IP += BitConverter.ToInt16(instruction.Operand, 0);
        }

        private static void iadd(Instruction instruction, Frame frame) {
            frame.Stack.Push(frame.Stack.Pop() + frame.Stack.Pop());
            nop(instruction, frame);
        }

        private static void iconst_m1(Instruction instruction, Frame frame) {
            frame.Stack.Push(-1);
            nop(instruction, frame);
        }

        private static void iconst_0(Instruction instruction, Frame frame) {
            frame.Stack.Push(0);
            nop(instruction, frame);
        }

        private static void iconst_1(Instruction instruction, Frame frame) {
            frame.Stack.Push(1);
            nop(instruction, frame);
        }

        private static void iconst_2(Instruction instruction, Frame frame) {
            frame.Stack.Push(2);
            nop(instruction, frame);
        }

        private static void iconst_3(Instruction instruction, Frame frame) {
            frame.Stack.Push(3);
            nop(instruction, frame);
        }

        private static void iconst_4(Instruction instruction, Frame frame) {
            frame.Stack.Push(4);
            nop(instruction, frame);
        }

        private static void iconst_5(Instruction instruction, Frame frame) {
            frame.Stack.Push(5);
            nop(instruction, frame);
        }

        private static void if_icmple(Instruction instruction, Frame frame) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f <= s) {
                @goto(instruction, frame);
            } else {
                nop(instruction, frame);
            }
        }

        private static void ifeq(Instruction instruction, Frame frame) {
            if(frame.Stack.Pop() == 0) {
                @goto(instruction, frame);
            } else {
                nop(instruction, frame);
            }
        }

        private static void iload(Instruction instruction, Frame frame) {
            frame.Stack.Push(frame.Locals[instruction.Operand[0]]);
            nop(instruction, frame);
        }

        private static void iload_0(Instruction instruction, Frame frame) {
            frame.Stack.Push(frame.Locals[0]);
            nop(instruction, frame);
        }

        private static void iload_1(Instruction instruction, Frame frame) {
            frame.Stack.Push(frame.Locals[1]);
            nop(instruction, frame);
        }

        private static void iload_2(Instruction instruction, Frame frame) {
            frame.Stack.Push(frame.Locals[2]);
            nop(instruction, frame);
        }

        private static void iload_3(Instruction instruction, Frame frame) {
            frame.Stack.Push(frame.Locals[3]);
            nop(instruction, frame);
        }

        private static void irem(Instruction instruction, Frame frame) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f % s);
            nop(instruction, frame);
        }

        private static void istore(Instruction instruction, Frame frame) {
            frame.Locals[instruction.Operand[0]] = frame.Stack.Pop();
            nop(instruction, frame);
        }

        private static void istore_0(Instruction instruction, Frame frame) {
            frame.Locals[0] = frame.Stack.Pop();
            nop(instruction, frame);
        }

        private static void istore_1(Instruction instruction, Frame frame) {
            frame.Locals[1] = frame.Stack.Pop();
            nop(instruction, frame);
        }

        private static void istore_2(Instruction instruction, Frame frame) {
            frame.Locals[2] = frame.Stack.Pop();
            nop(instruction, frame);
        }

        private static void istore_3(Instruction instruction, Frame frame) {
            frame.Locals[3] = frame.Stack.Pop();
            nop(instruction, frame);
        }

        private static void nop(Instruction instruction, Frame frame) {
            frame.IP += 1 + (int)instruction.OpCode.OperandSize;
        }

        private static void sipush(Instruction instruction, Frame frame) {
            frame.Stack.Push(BitConverter.ToInt16(instruction.Operand, 0));
            nop(instruction, frame);
        }

        private static void @return(Instruction instruction, Frame frame) {
            frame.IsReturned = true;
        }

        private static void da_oni_nikto_epta(Instruction instruction, Frame frame) {
            Console.WriteLine(frame.Stack.Pop());
            nop(instruction, frame);
        }
    }
}
