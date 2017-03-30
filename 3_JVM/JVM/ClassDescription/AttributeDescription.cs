using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class AttributeDescription {
        public ushort AttributeNameIndex;
        public uint AttributeLength;
        public byte[] Info;

        private AttributeDescription() { }

        public static AttributeDescription ParseData(byte[] data, ref int pos) {
            AttributeDescription res = new AttributeDescription();
            res.AttributeNameIndex = Utils.ReadUShort(data, ref pos);
            res.AttributeLength = Utils.ReadUInt(data, ref pos);
            res.Info = new byte[res.AttributeLength];
            for(uint i = 0; i < res.AttributeLength; ++i) {
                res.Info[i] = data[pos++];
            }
            return res;
        }
    }
}
