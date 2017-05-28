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

        public AttributeDescription(ushort attributeNameIndex, uint attributeLenght, byte[] info) {
            AttributeNameIndex = attributeNameIndex;
            AttributeLength = attributeLenght;
            Info = info;
        }

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

        public byte[] BuildData() {
            var res = new List<byte>();
            res.AddRange(Utils.WriteUShort(AttributeNameIndex));
            res.AddRange(Utils.WriteUInt(AttributeLength));
            res.AddRange(Info);
            return res.ToArray();
        }
    }
}
