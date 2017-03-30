using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.Runner {
    public class OpCodes {
        public static readonly IList<OpCode> ALL = new List<OpCode>() {
            new OpCode { Name = OpCodeName.bipush, Value = 0x10, OperandSize = 1 },
            new OpCode { Name = OpCodeName.d2f, Value = 0x90, OperandSize = 0 },
            new OpCode { Name = OpCodeName.d2i, Value = 0x8e, OperandSize = 0 },
            new OpCode { Name = OpCodeName.d2l, Value = 0x8f, OperandSize = 0 },
            new OpCode { Name = OpCodeName.@goto, Value = 0xa7, OperandSize = 2 },
            new OpCode { Name = OpCodeName.iadd, Value = 0x60, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_m1, Value = 0x02, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_0, Value = 0x03, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_1, Value = 0x04, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_2, Value = 0x05, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_3, Value = 0x06, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_4, Value = 0x07, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iconst_5, Value = 0x08, OperandSize = 0 },
            new OpCode { Name = OpCodeName.if_icmple, Value = 0xa4, OperandSize = 2 },
            new OpCode { Name = OpCodeName.ifeq, Value = 0x99, OperandSize = 2 },
            new OpCode { Name = OpCodeName.iload, Value = 0x15, OperandSize = 1 },
            new OpCode { Name = OpCodeName.iload_0, Value = 0x1a, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iload_1, Value = 0x1b, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iload_2, Value = 0x1c, OperandSize = 0 },
            new OpCode { Name = OpCodeName.iload_3, Value = 0x1d, OperandSize = 0 },
            new OpCode { Name = OpCodeName.irem, Value = 0x70, OperandSize = 0 },
            new OpCode { Name = OpCodeName.istore, Value = 0x36, OperandSize = 1 },
            new OpCode { Name = OpCodeName.istore_0, Value = 0x3b, OperandSize = 0 },
            new OpCode { Name = OpCodeName.istore_1, Value = 0x3c, OperandSize = 0 },
            new OpCode { Name = OpCodeName.istore_2, Value = 0x3d, OperandSize = 0 },
            new OpCode { Name = OpCodeName.istore_3, Value = 0x3e, OperandSize = 0 },
            new OpCode { Name = OpCodeName.nop, Value = 0x00, OperandSize = 0 },
            new OpCode { Name = OpCodeName.@return, Value = 0xb1, OperandSize = 0 },
            new OpCode { Name = OpCodeName.sipush, Value = 0x11, OperandSize = 2 },

            new OpCode { Name = OpCodeName.da_oni_nikto_epta, Value = 0xee, OperandSize = 0 },
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
