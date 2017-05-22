using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.Runner {
    public class OpCodes {
        public static readonly IList<OpCode> ALL = new List<OpCode>() {
            new OpCode { Name = OpCodeName.bipush, Value = 0x10, OperandSize = 1, StackChange = 1 },
            new OpCode { Name = OpCodeName.d2f, Value = 0x90, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.d2i, Value = 0x8e, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.d2l, Value = 0x8f, OperandSize = 0, StackChange = 0 },
            new OpCode { Name = OpCodeName.dadd, Value = 0x63, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dcmpg, Value = 0x98, OperandSize = 0, StackChange = -3 },
            new OpCode { Name = OpCodeName.dcmpl, Value = 0x97, OperandSize = 0, StackChange = -3 },
            new OpCode { Name = OpCodeName.dconst_0, Value = 0x0e, OperandSize = 0, StackChange = 2 },
            new OpCode { Name = OpCodeName.dconst_1, Value = 0x0f, OperandSize = 0, StackChange = 2 },
            new OpCode { Name = OpCodeName.ddiv, Value = 0x6f, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dload, Value = 0x18, OperandSize = 1, StackChange = 2 },
            new OpCode { Name = OpCodeName.dload_0, Value = 0x26, OperandSize = 0, StackChange = 2 },
            new OpCode { Name = OpCodeName.dload_1, Value = 0x27, OperandSize = 0, StackChange = 2 },
            new OpCode { Name = OpCodeName.dload_2, Value = 0x28, OperandSize = 0, StackChange = 2 },
            new OpCode { Name = OpCodeName.dload_3, Value = 0x29, OperandSize = 0, StackChange = 2 },
            new OpCode { Name = OpCodeName.dmul, Value = 0x6b, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.drem, Value = 0x73, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dstore, Value = 0x39, OperandSize = 1, StackChange = -2 },
            new OpCode { Name = OpCodeName.dstore_0, Value = 0x47, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dstore_1, Value = 0x48, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dstore_2, Value = 0x49, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dstore_3, Value = 0x4a, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.dsub, Value = 0x67, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.@goto, Value = 0xa7, OperandSize = 2, StackChange = 0 },
            new OpCode { Name = OpCodeName.i2d, Value = 0x87, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iadd, Value = 0x60, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.iconst_m1, Value = 0x02, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iconst_0, Value = 0x03, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iconst_1, Value = 0x04, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iconst_2, Value = 0x05, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iconst_3, Value = 0x06, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iconst_4, Value = 0x07, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iconst_5, Value = 0x08, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.idiv, Value = 0x6c, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.imul, Value = 0x68, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.isub, Value = 0x64, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.if_icmple, Value = 0xa4, OperandSize = 2, StackChange = -2 },
            new OpCode { Name = OpCodeName.ifeq, Value = 0x99, OperandSize = 2, StackChange = -1 },
            new OpCode { Name = OpCodeName.ifne, Value = 0x9a, OperandSize = 2, StackChange = -1 },
            new OpCode { Name = OpCodeName.iload, Value = 0x15, OperandSize = 1, StackChange = 1 },
            new OpCode { Name = OpCodeName.iload_0, Value = 0x1a, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iload_1, Value = 0x1b, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iload_2, Value = 0x1c, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.iload_3, Value = 0x1d, OperandSize = 0, StackChange = 1 },
            new OpCode { Name = OpCodeName.irem, Value = 0x70, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.istore, Value = 0x36, OperandSize = 1, StackChange = -1 },
            new OpCode { Name = OpCodeName.istore_0, Value = 0x3b, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.istore_1, Value = 0x3c, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.istore_2, Value = 0x3d, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.istore_3, Value = 0x3e, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.ldc, Value = 0x12, OperandSize = 1, StackChange = 1 },
            new OpCode { Name = OpCodeName.ldc_w, Value = 0x13, OperandSize = 2, StackChange = 1 },
            new OpCode { Name = OpCodeName.ldc2_w, Value = 0x14, OperandSize = 2, StackChange = 2 },
            new OpCode { Name = OpCodeName.nop, Value = 0x00, OperandSize = 0, StackChange = 0 },
            new OpCode { Name = OpCodeName.@return, Value = 0xb1, OperandSize = 0, StackChange = 0 },
            new OpCode { Name = OpCodeName.sipush, Value = 0x11, OperandSize = 2, StackChange = 1 },

            new OpCode { Name = OpCodeName.da_oni_nikto_epta_i, Value = 0xee, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.da_oni_nikto_epta_f, Value = 0xed, OperandSize = 0, StackChange = -1 },
            new OpCode { Name = OpCodeName.da_oni_nikto_epta_d, Value = 0xec, OperandSize = 0, StackChange = -2 },
            new OpCode { Name = OpCodeName.da_oni_nikto_epta_l, Value = 0xeb, OperandSize = 0, StackChange = -2 },
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
