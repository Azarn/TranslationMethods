using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.OpCodes {
    public class OpCodes {
        public static readonly IList<OpCode> ALL = new List<OpCode>() {
            new OpCode { Name = OpCodeName.aconst_null, Value = 0x01, OperandSize = 0 },
            new OpCode { Name = OpCodeName.aload, Value = 0x19, OperandSize = 1 },
            new OpCode { Name = OpCodeName.aload_0, Value = 0x2a, OperandSize = 0 },
            new OpCode { Name = OpCodeName.aload_1, Value = 0x2b, OperandSize = 0 },
            new OpCode { Name = OpCodeName.aload_2, Value = 0x2c, OperandSize = 0 },
            new OpCode { Name = OpCodeName.aload_3, Value = 0x2d, OperandSize = 0 },
        };

        public static readonly IDictionary<byte, OpCode> VALUE_TO_OPCODE_MAP = new Dictionary<byte, OpCode>();
        public static readonly IDictionary<OpCodeName, OpCode> NAME_TO_OPCODE_MAP = new Dictionary<OpCodeName, OpCode>();

        static OpCodes() {
            foreach(OpCode op in ALL) {
                VALUE_TO_OPCODE_MAP.Add(op.Value, op);
                NAME_TO_OPCODE_MAP.Add(op.Name, op);
            }
        }
    }
}
