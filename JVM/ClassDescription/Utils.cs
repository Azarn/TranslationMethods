using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class Utils {
        public static short ReadShort(byte[] data, ref int pos) {
            short res = (short)((data[pos] << 8) + data[pos + 1]);
            pos += 2;
            return res;
        }

        public static uint ReadUInt(byte[] data, ref int pos) {
            uint res = (uint)(ReadShort(data, ref pos) << 16) + (uint)ReadShort(data, ref pos);
            return res;
        }
    }
}
