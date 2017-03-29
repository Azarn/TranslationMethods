using System;
using System.Linq;
using System.Collections.Generic;

namespace JVM.Runner {
    public class Frame {
        public Stack<int> Stack;
        public List<int> Locals;
        public int IP;
        public bool IsReturned;

        public Frame(int MaxStack, int MaxLocals) {
            IP = 0;
            Stack = new Stack<int>(MaxStack);
            Locals = new List<int>(new int[MaxLocals]);
            IsReturned = false;
        }

        public long GetLongFromStack() {
            long res = Stack.Pop();
            res <<= 32;
            res += Stack.Pop();
            return res;
        }

        public void PutLongToStack(long value) {
            Stack.Push((int)(value & 0xFFFFFFFF));
            Stack.Push((int)(value >> 32));
        }

        public float GetFloatFromStack() {
            return BitConverter.ToSingle(BitConverter.GetBytes(Stack.Pop()), 0);
        }

        public void PutFloatToStack(float value) {
            Stack.Push(BitConverter.ToInt32(BitConverter.GetBytes(value), 0));
        }

        public double GetDoubleFromStack() {
            return BitConverter.Int64BitsToDouble(GetLongFromStack());
        }

        public void PutDoubleToStack(double value) {
            PutLongToStack(BitConverter.ToInt64(BitConverter.GetBytes(value), 0));
        }
    }
}
