using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class MethodInfo {
        public short AccessFlags;
        public short NameIndex;
        public short DescriptorIndex;
        public short AttributesCount;
        public AttributeDescription[] Attributes;

        private MethodInfo() { }

        public static MethodInfo ParseData(byte[] data, ref int pos) {
            MethodInfo res = new MethodInfo();
            res.AccessFlags = Utils.ReadShort(data, ref pos);
            res.NameIndex = Utils.ReadShort(data, ref pos);
            res.DescriptorIndex = Utils.ReadShort(data, ref pos);
            res.AttributesCount = Utils.ReadShort(data, ref pos);
            res.Attributes = new AttributeDescription[res.AttributesCount];
            for (uint i = 0; i < res.AttributesCount; ++i) {
                res.Attributes[i] = AttributeDescription.ParseData(data, ref pos);
            }
            return res;
        }
    }
}
