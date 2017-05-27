using System;
using System.Collections.Generic;
using JVM.ClassDescription;

namespace JVM.Runner {
    public static class InstructionExecutor {
        public delegate void ExecuteAction(Instruction instruction, Frame frame, ClassFile cFile);

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
            { OpCodeName.dmul, dmul },
            { OpCodeName.drem, drem },
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
            { OpCodeName.imul, imul},
            { OpCodeName.ineg, ineg},
            { OpCodeName.isub, isub},
            { OpCodeName.ixor, ixor},
            { OpCodeName.ifeq, ifeq },
            { OpCodeName.ifge, ifge },
            { OpCodeName.ifgt, ifgt },
            { OpCodeName.ifle, ifle },
            { OpCodeName.iflt, iflt },
            { OpCodeName.ifne, ifne },
            { OpCodeName.if_icmpeq, if_icmpeq },
            { OpCodeName.if_icmpne, if_icmpne },
            { OpCodeName.if_icmpge, if_icmpge },
            { OpCodeName.if_icmpgt, if_icmpgt },
            { OpCodeName.if_icmple, if_icmple },
            { OpCodeName.if_icmplt, if_icmplt },
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
            { OpCodeName.ldc, ldc },
            { OpCodeName.ldc_w, ldc_w },
            { OpCodeName.ldc2_w, ldc2_w },
            { OpCodeName.nop, nop },
            { OpCodeName.@return, @return },
            { OpCodeName.sipush, sipush },

            { OpCodeName.da_oni_nikto_epta_i, da_oni_nikto_epta_i },
            { OpCodeName.da_oni_nikto_epta_f, da_oni_nikto_epta_f },
            { OpCodeName.da_oni_nikto_epta_d, da_oni_nikto_epta_d },
            { OpCodeName.da_oni_nikto_epta_l, da_oni_nikto_epta_l }
        };

        private static void bipush(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(instruction.Operand[0]);
            nop(instruction, frame, cFile);
        }

        private static void d2f(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutFloatToStack((float)frame.GetDoubleFromStack());
            nop(instruction, frame, cFile);
        }

        private static void d2i(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push((int)frame.GetDoubleFromStack());
            nop(instruction, frame, cFile);
        }

        private static void d2l(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutLongToStack((long)frame.GetDoubleFromStack());
            nop(instruction, frame, cFile);
        }

        private static void dadd(Instruction instruction, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f + s);
            nop(instruction, frame, cFile);
        }

        private static void dcmpg(Instruction instruction, Frame frame, ClassFile cFile) {
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
            nop(instruction, frame, cFile);
        }

        private static void dcmpl(Instruction instruction, Frame frame, ClassFile cFile) {
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
            nop(instruction, frame, cFile);
        }

        private static void dconst_0(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(0.0);
            nop(instruction, frame, cFile);
        }

        private static void dconst_1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(1.0);
            nop(instruction, frame, cFile);
        }

        private static void ddiv(Instruction instruction, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f / s);
            nop(instruction, frame, cFile);
        }

        private static void dload(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(instruction.Operand[0]));
            nop(instruction, frame, cFile);
        }

        private static void dload_0(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(0));
            nop(instruction, frame, cFile);
        }

        private static void dload_1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(1));
            nop(instruction, frame, cFile);
        }

        private static void dload_2(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(2));
            nop(instruction, frame, cFile);
        }

        private static void dload_3(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.GetDoubleFromLocals(3));
            nop(instruction, frame, cFile);
        }

        private static void dmul(Instruction instruction, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f * s);
            nop(instruction, frame, cFile);
        }

        private static void drem(Instruction instruction, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f % s);
            nop(instruction, frame, cFile);
        }

        private static void dstore(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), instruction.Operand[0]);
            nop(instruction, frame, cFile);
        }

        private static void dstore_0(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 0);
            nop(instruction, frame, cFile);
        }

        private static void dstore_1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 1);
            nop(instruction, frame, cFile);
        }

        private static void dstore_2(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 2);
            nop(instruction, frame, cFile);
        }

        private static void dstore_3(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToLocals(frame.GetDoubleFromStack(), 3);
            nop(instruction, frame, cFile);
        }

        private static void dsub(Instruction instruction, Frame frame, ClassFile cFile) {
            double s = frame.GetDoubleFromStack();
            double f = frame.GetDoubleFromStack();
            frame.PutDoubleToStack(f - s);
            nop(instruction, frame, cFile);
        }

        private static void @goto(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.IP += BitConverter.ToInt16(instruction.Operand, 0);
        }

        private static void i2d(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.PutDoubleToStack(frame.Stack.Pop());
            nop(instruction, frame, cFile);
        }

        private static void iadd(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Stack.Pop() + frame.Stack.Pop());
            nop(instruction, frame, cFile);
        }

        private static void iconst_m1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(-1);
            nop(instruction, frame, cFile);
        }

        private static void iconst_0(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(0);
            nop(instruction, frame, cFile);
        }

        private static void iconst_1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(1);
            nop(instruction, frame, cFile);
        }

        private static void iconst_2(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(2);
            nop(instruction, frame, cFile);
        }

        private static void iconst_3(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(3);
            nop(instruction, frame, cFile);
        }

        private static void iconst_4(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(4);
            nop(instruction, frame, cFile);
        }

        private static void iconst_5(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(5);
            nop(instruction, frame, cFile);
        }

        private static void idiv(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f / s);
            nop(instruction, frame, cFile);
        }

        private static void if_icmpeq(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f == s) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void if_icmpne(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f != s) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void if_icmpge(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f >= s) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void if_icmpgt(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f > s) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void if_icmple(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f <= s) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void if_icmplt(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            if (f < s) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void ifeq(Instruction instruction, Frame frame, ClassFile cFile) {
            if(frame.Stack.Pop() == 0) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void ifge(Instruction instruction, Frame frame, ClassFile cFile) {
            if (frame.Stack.Pop() >= 0) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void ifgt(Instruction instruction, Frame frame, ClassFile cFile) {
            if (frame.Stack.Pop() > 0) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void ifle(Instruction instruction, Frame frame, ClassFile cFile) {
            if (frame.Stack.Pop() <= 0) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void iflt(Instruction instruction, Frame frame, ClassFile cFile) {
            if (frame.Stack.Pop() < 0) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }


        private static void ifne(Instruction instruction, Frame frame, ClassFile cFile) {
            if (frame.Stack.Pop() != 0) {
                @goto(instruction, frame, cFile);
            } else {
                nop(instruction, frame, cFile);
            }
        }

        private static void iload(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[instruction.Operand[0]]);
            nop(instruction, frame, cFile);
        }

        private static void iload_0(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[0]);
            nop(instruction, frame, cFile);
        }

        private static void iload_1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[1]);
            nop(instruction, frame, cFile);
        }

        private static void iload_2(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[2]);
            nop(instruction, frame, cFile);
        }

        private static void iload_3(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(frame.Locals[3]);
            nop(instruction, frame, cFile);
        }

        private static void irem(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f % s);
            nop(instruction, frame, cFile);
        }

        private static void imul(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f * s);
            nop(instruction, frame, cFile);
        }

        private static void ineg(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(-frame.Stack.Pop());
            nop(instruction, frame, cFile);
        }

        private static void isub(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f / s);
            nop(instruction, frame, cFile);
        }

        private static void ixor(Instruction instruction, Frame frame, ClassFile cFile) {
            int s = frame.Stack.Pop();
            int f = frame.Stack.Pop();
            frame.Stack.Push(f ^ s);
            nop(instruction, frame, cFile);
        }

        private static void istore(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Locals[instruction.Operand[0]] = frame.Stack.Pop();
            nop(instruction, frame, cFile);
        }

        private static void istore_0(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Locals[0] = frame.Stack.Pop();
            nop(instruction, frame, cFile);
        }

        private static void istore_1(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Locals[1] = frame.Stack.Pop();
            nop(instruction, frame, cFile);
        }

        private static void istore_2(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Locals[2] = frame.Stack.Pop();
            nop(instruction, frame, cFile);
        }

        private static void istore_3(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Locals[3] = frame.Stack.Pop();
            nop(instruction, frame, cFile);
        }

        private static void ldc(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(((CONSTANT_B4_info)cFile.GetConstant(instruction.Operand[0]).Info).ToInt());
            nop(instruction, frame, cFile);
        }

        private static void ldc_w(Instruction instruction, Frame frame, ClassFile cFile) {
            ushort index = BitConverter.ToUInt16(instruction.Operand, 0);
            frame.Stack.Push(((CONSTANT_B4_info)cFile.GetConstant(index).Info).ToInt());
            nop(instruction, frame, cFile);
        }

        private static void ldc2_w(Instruction instruction, Frame frame, ClassFile cFile) {
            ushort index = BitConverter.ToUInt16(instruction.Operand, 0);
            frame.PutLongToStack(((CONSTANT_B8_info)cFile.GetConstant(index).Info).ToLong());
            nop(instruction, frame, cFile);
        }

        private static void nop(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.IP += (int)instruction.OpCode.OpCodeSize;
        }

        private static void sipush(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.Stack.Push(BitConverter.ToInt16(instruction.Operand, 0));
            nop(instruction, frame, cFile);
        }

        private static void @return(Instruction instruction, Frame frame, ClassFile cFile) {
            frame.IsReturned = true;
        }

        private static void da_oni_nikto_epta_i(Instruction instruction, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.Stack.Pop());
            nop(instruction, frame, cFile);
        }

        private static void da_oni_nikto_epta_f(Instruction instruction, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.GetFloatFromStack());
            nop(instruction, frame, cFile);
        }

        private static void da_oni_nikto_epta_d(Instruction instruction, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.GetDoubleFromStack());
            nop(instruction, frame, cFile);
        }

        private static void da_oni_nikto_epta_l(Instruction instruction, Frame frame, ClassFile cFile) {
            Console.WriteLine(frame.GetLongFromStack());
            nop(instruction, frame, cFile);
        }
    }
}
