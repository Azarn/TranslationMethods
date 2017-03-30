using System;
using System.Collections.Generic;
using JVM.ClassDescription;

namespace JVM.Runner {
    public static class InstructionExecutor {
        public delegate void ExecuteAction(byte[] operand, Frame frame, ClassFile cFile);

        public static IDictionary<OpCodeName, ExecuteAction> EXECUTORS_MAP = new Dictionary<OpCodeName, ExecuteAction>() {
            { OpCodeName.bipush, bipush },
            { OpCodeName.d2f, d2f },
            { OpCodeName.d2i, d2i },
            { OpCodeName.d2l, d2l },
            { OpCodeName.ddiv, ddiv },
            { OpCodeName.dadd, dadd },
            { OpCodeName.dcmpg, dcmpg },
            { OpCodeName.dcmpl, dcmpl },
            { OpCodeName.dconst_0, dconst_0 },
            { OpCodeName.dconst_1, dconst_1 },
            { OpCodeName.dload, dload },
            { OpCodeName.dload_0, dload_0 },
            { OpCodeName.dload_1, dload_1 },
            { OpCodeName.dload_2, dload_2 },
            { OpCodeName.dload_3, dload_3 },
            { OpCodeName.dstore, dstore },
            { OpCodeName.dstore_0, dstore_0 },
            { OpCodeName.dstore_1, dstore_1 },
            { OpCodeName.dstore_2, dstore_2 },
            { OpCodeName.dstore_3, dstore_3 },
            { OpCodeName.dsub, dsub },
            { OpCodeName.@goto, @goto },
            { OpCodeName.i2d, i2d },
            { OpCodeName.iadd, iadd },
            { OpCodeName.iconst_m1, iconst_m1 },
            { OpCodeName.iconst_0, iconst_0 },
            { OpCodeName.iconst_1, iconst_1 },
            { OpCodeName.iconst_2, iconst_2 },
            { OpCodeName.iconst_3, iconst_3 },
            { OpCodeName.iconst_4, iconst_4 },
            { OpCodeName.iconst_5, iconst_5 },
            { OpCodeName.idiv, idiv },
            { OpCodeName.ifeq, ifeq },
            { OpCodeName.ifne, ifne },
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
            { OpCodeName.ldc2_w, ldc2_w },
            { OpCodeName.nop, nop },
            { OpCodeName.@return, @return },
            { OpCodeName.sipush, sipush },

            { OpCodeName.da_oni_nikto_epta_i, da_oni_nikto_epta_i },
            { OpCodeName.da_oni_nikto_epta_f, da_oni_nikto_epta_f },
            { OpCodeName.da_oni_nikto_epta_d, da_oni_nikto_epta_d },
            { OpCodeName.da_oni_nikto_epta_l, da_oni_nikto_epta_l }
        };

        private static void bipush(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(operand[0]);
            nop(operand, frame, cFile);
        }

        private static void d2f(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutFloatToStack((float)frame.GetDoubleFromStack());
            nop(operand, frame, cFile);
        }

        private static void d2i(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push((int)frame.GetDoubleFromStack());
            nop(operand, frame, cFile);
        }

        private static void d2l(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutLongToStack((long)frame.GetDoubleFromStack());
            nop(operand, frame, cFile);
        }

        private static void dadd(byte[] operand, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f + s);
            nop(operand, frame, cFile);
        }

        private static void dcmpg(byte[] operand, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            int res = 0;
            if (double.IsNaN(f) || double.IsNaN(s)) {
                res = 1;
            } else {
                res = f.CompareTo(s);
                if (res > 1) {
                    res = 1;
                } else if (res < -1) {
                    res = -1;
                }
            }
            frame.Stack.Push(res);
            nop(operand, frame, cFile);
        }

        private static void dcmpl(byte[] operand, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            int res = 0;
            if (double.IsNaN(f) || double.IsNaN(s)) {
                res = -1;
            } else {
                res = f.CompareTo(s);
                if (res > 1) {
                    res = 1;
                } else if (res < -1) {
                    res = -1;
                }
            }
            frame.Stack.Push(res);
            nop(operand, frame, cFile);
        }

        private static void dconst_0(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(0.0);
            nop(operand, frame, cFile);
        }

        private static void dconst_1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(1.0);
            nop(operand, frame, cFile);
        }

        private static void ddiv(byte[] operand, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f / s);
            nop(operand, frame, cFile);
        }

        private static void dload(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(operand[0]));
            nop(operand, frame, cFile);
        }

        private static void dload_0(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(0));
            nop(operand, frame, cFile);
        }

        private static void dload_1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(1));
            nop(operand, frame, cFile);
        }

        private static void dload_2(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(2));
            nop(operand, frame, cFile);
        }

        private static void dload_3(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(3));
            nop(operand, frame, cFile);
        }

        private static void dstore(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), operand[0]);
            nop(operand, frame, cFile);
        }

        private static void dstore_0(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 0);
            nop(operand, frame, cFile);
        }

        private static void dstore_1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 1);
            nop(operand, frame, cFile);
        }

        private static void dstore_2(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 2);
            nop(operand, frame, cFile);
        }

        private static void dstore_3(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 3);
            nop(operand, frame, cFile);
        }

        private static void dsub(byte[] operand, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f - s);
            nop(operand, frame, cFile);
        }

        private static void @goto(byte[] operand, Frame frame, ClassFile cFile) {
            frame.IP += BitConverter.ToInt16(operand, 0);
        }

        private static void i2d(byte[] operand, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.Stack.Pop());
            nop(operand, frame, cFile);
        }

        private static void iadd(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Stack.Pop() + frame.Stack.Pop());
            nop(operand, frame, cFile);
        }

        private static void iconst_m1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(-1);
            nop(operand, frame, cFile);
        }

        private static void iconst_0(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(0);
            nop(operand, frame, cFile);
        }

        private static void iconst_1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(1);
            nop(operand, frame, cFile);
        }

        private static void iconst_2(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(2);
            nop(operand, frame, cFile);
        }

        private static void iconst_3(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(3);
            nop(operand, frame, cFile);
        }

        private static void iconst_4(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(4);
            nop(operand, frame, cFile);
        }

        private static void iconst_5(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(5);
            nop(operand, frame, cFile);
        }

        private static void idiv(byte[] operand, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f / s);
            nop(operand, frame, cFile);
        }

        private static void if_icmple(byte[] operand, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f <= s) {
                @goto(operand, frame, cFile);
            } else {
                nop(operand, frame, cFile);
            }
        }

        private static void ifeq(byte[] operand, Frame frame, ClassFile cFile) {
            if(frame.Stack.Pop() == 0) {
                @goto(operand, frame, cFile);
            } else {
                nop(operand, frame, cFile);
            }
        }

        private static void ifne(byte[] operand, Frame frame, ClassFile cFile) {
            if (frame.Stack.Pop() != 0) {
                @goto(operand, frame, cFile);
            } else {
                nop(operand, frame, cFile);
            }
        }

        private static void iload(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[operand[0]]);
            nop(operand, frame, cFile);
        }

        private static void iload_0(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[0]);
            nop(operand, frame, cFile);
        }

        private static void iload_1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[1]);
            nop(operand, frame, cFile);
        }

        private static void iload_2(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[2]);
            nop(operand, frame, cFile);
        }

        private static void iload_3(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[3]);
            nop(operand, frame, cFile);
        }

        private static void irem(byte[] operand, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f % s);
            nop(operand, frame, cFile);
        }

        private static void istore(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Locals[operand[0]] = frame.Stack.Pop();
            nop(operand, frame, cFile);
        }

        private static void istore_0(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Locals[0] = frame.Stack.Pop();
            nop(operand, frame, cFile);
        }

        private static void istore_1(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Locals[1] = frame.Stack.Pop();
            nop(operand, frame, cFile);
        }

        private static void istore_2(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Locals[2] = frame.Stack.Pop();
            nop(operand, frame, cFile);
        }

        private static void istore_3(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Locals[3] = frame.Stack.Pop();
            nop(operand, frame, cFile);
        }

        private static void nop(byte[] operand, Frame frame, ClassFile cFile) {
            frame.IP += 1 + operand.Length;
        }

        private static void ldc2_w(byte[] operand, Frame frame, ClassFile cFile) {
            ushort index = BitConverter.ToUInt16(operand, 0);
            frame.PutLongToStack(((CONSTANT_B8_info)cFile.GetConstant(index).Info).ToLong());
            nop(operand, frame, cFile);
        }

        private static void sipush(byte[] operand, Frame frame, ClassFile cFile) {
            frame.Stack.Push(BitConverter.ToInt16(operand, 0));
            nop(operand, frame, cFile);
        }

        private static void @return(byte[] operand, Frame frame, ClassFile cFile) {
            frame.IsReturned = true;
        }

        private static void da_oni_nikto_epta_i(byte[] operand, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.Stack.Pop());
            nop(operand, frame, cFile);
        }

        private static void da_oni_nikto_epta_f(byte[] operand, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.GetFloatFromStack());
            nop(operand, frame, cFile);
        }

        private static void da_oni_nikto_epta_d(byte[] operand, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.GetDoubleFromStack());
            nop(operand, frame, cFile);
        }

        private static void da_oni_nikto_epta_l(byte[] operand, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.GetLongFromStack());
            nop(operand, frame, cFile);
        }
    }
}
