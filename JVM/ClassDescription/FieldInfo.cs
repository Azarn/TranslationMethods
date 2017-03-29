using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class FieldInfo {
        public ushort AccessFlags;
        public ushort NameIndex;
        public ushort DescriptorIndex;
        public ushort AttributesCount;
        public AttributeDescription[] Attributes;

        public IDictionary<string, object> AttributeParsers;

        private FieldInfo() { }

        public static FieldInfo ParseData(ClassFile cFile, byte[] data, ref int pos) {
            FieldInfo res = new FieldInfo();
            res.AccessFlags = Utils.ReadUShort(data, ref pos);
            res.NameIndex = Utils.ReadUShort(data, ref pos);
            res.DescriptorIndex = Utils.ReadUShort(data, ref pos);
            res.AttributesCount = Utils.ReadUShort(data, ref pos);
            res.Attributes = new AttributeDescription[res.AttributesCount];
            for (uint i = 0; i < res.AttributesCount; ++i) {
                res.Attributes[i] = AttributeDescription.ParseData(data, ref pos);
            }

            res.AttributeParsers = AttributeParser.GenerateAttributeMap(cFile, res.Attributes);
            return res;
        }
    }
}
