using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public static class Utils {
        public static ushort ReadUShort(byte[] data, ref int pos) {
            ushort res = (ushort)((data[pos] << 8) + data[pos + 1]);
            pos += 2;
            return res;
        }

        public static uint ReadUInt(byte[] data, ref int pos) {
            uint res = (uint)(ReadUShort(data, ref pos) << 16) + (uint)ReadUShort(data, ref pos);
            return res;
        }
    }
}
