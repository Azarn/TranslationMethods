using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.Runner {
    public class Instruction {
        public OpCode OpCode;
        public byte[] Operand;

        public override string ToString() {
            return string.Format("{0} <{1}>", OpCode.Name, Operand.Length);
        }
    }
}
