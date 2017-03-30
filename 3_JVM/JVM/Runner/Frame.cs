using System;
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

        private long GetLongFromTwoInt32(int f, int s) {
            return (long)(((ulong)f << 32) + (uint)s);
        }

        private int[] GetTwoInt32FromLong(long value) {
            return new int[2] { (int)(value >> 32), (int)(value & 0xFFFFFFFF), };
        }

        private float GetFloatFromInt32(int value) {
            return BitConverter.ToSingle(BitConverter.GetBytes(value), 0);
        }

        private int GetInt32FromFloat(float value) {
            return BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
        }

        private double GetDoubleFromLong(long value) {
            return BitConverter.Int64BitsToDouble(value);
        }

        private long GetLongFromDouble(double value) {
            return BitConverter.ToInt64(BitConverter.GetBytes(value), 0);
        }

        public long GetLongFromStack() {
            return GetLongFromTwoInt32(Stack.Pop(), Stack.Pop());
        }

        public long GetLongFromLocals(int index) {
            return GetLongFromTwoInt32(Locals[index], Locals[index + 1]);
        }

        public void PutLongToStack(long value) {
            int[] data = GetTwoInt32FromLong(value);
            Stack.Push(data[1]);
            Stack.Push(data[0]);
        }

        public void PutLongToLocals(long value, int index) {
            int[] data = GetTwoInt32FromLong(value);
            Locals[index] = data[0];
            Locals[index + 1] = data[1];
        }

        public float GetFloatFromStack() {
            return GetFloatFromInt32(Stack.Pop());
        }

        public float GetFloatFromLocals(int index) {
            return GetFloatFromInt32(Locals[index]);
        }

        public void PutFloatToStack(float value) {
            Stack.Push(GetInt32FromFloat(value));
        }

        public void PutFloatToLocals(float value, int index) {
            Locals[index] = GetInt32FromFloat(value);
        }

        public double GetDoubleFromStack() {
            return GetDoubleFromLong(GetLongFromStack());
        }

        public double GetDoubleFromLocals(int index) {
            return GetDoubleFromLong(GetLongFromLocals(index));
        }

        public void PutDoubleToStack(double value) {
            PutLongToStack(GetLongFromDouble(value));
        }

        public void PutDoubleToLocals(double value, int index) {
            PutLongToLocals(GetLongFromDouble(value), index);
        }
    }
}
