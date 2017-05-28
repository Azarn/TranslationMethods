using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class MethodInfo {
        public ushort AccessFlags;
        public ushort NameIndex;
        public ushort DescriptorIndex;
        public ushort AttributesCount = 0;
        public AttributeDescription[] Attributes = new AttributeDescription[0];

        public IDictionary<string, object> AttributeParsers;

        private MethodInfo() { }

        public MethodInfo(MethodAccessFlags accessFlags, ushort nameIndex, ushort descriptorIndex) {
            AccessFlags = (ushort)accessFlags;
            NameIndex = nameIndex;
            DescriptorIndex = descriptorIndex;
        }

        public static MethodInfo ParseData(ClassFile cFile, byte[] data, ref int pos) {
            MethodInfo res = new MethodInfo();
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

        public byte[] BuildData() {
            var res = new List<byte>();
            res.AddRange(Utils.WriteUShort(AccessFlags));
            res.AddRange(Utils.WriteUShort(NameIndex));
            res.AddRange(Utils.WriteUShort(DescriptorIndex));
            res.AddRange(Utils.WriteUShort(AttributesCount));
            foreach(var attribute in Attributes) {
                res.AddRange(attribute.BuildData());
            }
            return res.ToArray();
        }
    }
}
