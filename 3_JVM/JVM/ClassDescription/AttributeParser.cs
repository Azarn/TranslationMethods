using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public static class AttributeParser {
        public static IDictionary<string, object> GenerateAttributeMap(ClassFile cFile, AttributeDescription[] attributes) {
            IDictionary<string, object> res = new Dictionary<string, object>();
            foreach(AttributeDescription ad in attributes) {
                string name = ((CONSTANT_Utf8_info)cFile.GetConstant(ad.AttributeNameIndex).Info).String;
                object parser = null;
                switch(name) {
                    case "Code":
                        parser = CodeAttributeParser.ParseData(cFile, ad.Info);
                        break;
                }
                res.Add(name, parser);
            }
            return res;
        }
    }

    public class CodeAttributeParser {
        public ushort MaxStack;
        public ushort MaxLocals;
        public uint CodeLength;
        public byte[] Code;
        public ushort ExceptionTableLength;
        public ExceptionTableDescription[] ExceptionTable;
        public ushort AttributesCount;
        public AttributeDescription[] Attributes;

        public IDictionary<string, object> AttributeParsers;

        private CodeAttributeParser() { }

        public static CodeAttributeParser ParseData(ClassFile cFile, byte[] data) {
            int pos = 0;
            CodeAttributeParser res = new CodeAttributeParser();
            res.MaxStack = Utils.ReadUShort(data, ref pos);
            res.MaxLocals = Utils.ReadUShort(data, ref pos);

            res.CodeLength = Utils.ReadUInt(data, ref pos);
            res.Code = new byte[res.CodeLength];
            for(int i = 0; i < res.CodeLength; ++i) {
                res.Code[i] = data[pos++];
            }

            res.ExceptionTableLength = Utils.ReadUShort(data, ref pos);
            res.ExceptionTable = new ExceptionTableDescription[res.ExceptionTableLength];
            for(int i = 0; i < res.ExceptionTableLength; ++i) {
                res.ExceptionTable[i] = ExceptionTableDescription.ParseData(data, ref pos);
            }

            res.AttributesCount = Utils.ReadUShort(data, ref pos);
            res.Attributes = new AttributeDescription[res.AttributesCount];
            for(int i = 0; i < res.AttributesCount; ++i) {
                res.Attributes[i] = AttributeDescription.ParseData(data, ref pos);
            }

            res.AttributeParsers = AttributeParser.GenerateAttributeMap(cFile, res.Attributes);
            return res;
        }
    }
}
