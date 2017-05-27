using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.Runner {
    public class Instruction {
        public OpCode OpCode;
        public byte[] Operand = new byte[0];

        public uint ByteSize => OpCode.OpCodeSize;

        public override string ToString() {
            if (Operand.Length == 0) {
                return OpCode.Name.ToString();
            }
            return string.Format("{0} <{1}>", OpCode.Name, BitConverter.ToString(Operand));
        }
    }
}
