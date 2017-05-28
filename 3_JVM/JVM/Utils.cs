using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM {
    public static class Utils {
        public static ushort ReadUShort(byte[] data, ref int pos) {
            ushort res = (ushort)((data[pos] << 8) + data[pos + 1]);
            pos += 2;
            return res;
        }

        public static byte[] WriteUShort(ushort value) {
            return new byte[2] {
                (byte)(value >> 8), (byte)value
            };
        }

        public static uint ReadUInt(byte[] data, ref int pos) {
            uint res = (uint)(ReadUShort(data, ref pos) << 16) + (uint)ReadUShort(data, ref pos);
            return res;
        }

        public static byte[] WriteUInt(uint value) {
            return WriteUShort((ushort)(value >> 16)).Concat(WriteUShort((ushort)value)).ToArray();
        }

        public static byte[] ReverseIfEndian(byte[] data) {
            if (BitConverter.IsLittleEndian) {
                Array.Reverse(data);
            }
            return data;
        }
    }
}
