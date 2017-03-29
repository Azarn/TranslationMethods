using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class ClassFile {
        public byte[] Magic = new byte[4];
        public ushort MinorVersion;
        public ushort MajorVersion;
        public ushort ConstantPoolCount;
        public ConstantPoolDescription[] ConstantPool;
        public ushort AccessFlags;
        public ushort ThisClass;
        public ushort SuperClass;
        public ushort InterfacesCount;
        public ushort[] Interfaces;
        public ushort FieldsCount;
        public FieldInfo[] Fields;
        public ushort MethodsCount;
        public MethodInfo[] Methods;
        public ushort AttributesCount;
        public AttributeDescription[] Attributes;

        public IDictionary<string, object> AttributeParsers;

        private ClassFile() { }

        public ConstantPoolDescription GetConstant(ushort index) {
            return ConstantPool[index - 1];
        }

        public static ClassFile ParseClassFile(byte[] data) {
            ClassFile res = new ClassFile();
            int pos = 0;
            for (int i = 0; i < 4; ++i) {
                res.Magic[i] = data[pos++];
            }

            res.MinorVersion = Utils.ReadUShort(data, ref pos);
            res.MajorVersion = Utils.ReadUShort(data, ref pos);

            res.ConstantPoolCount = Utils.ReadUShort(data, ref pos);
            res.ConstantPool = new ConstantPoolDescription[res.ConstantPoolCount - 1];
            for (int i = 0; i < res.ConstantPoolCount - 1; ++i) {
                res.ConstantPool[i] = ConstantPoolDescription.ParseData(data, ref pos);
            }

            res.AccessFlags = Utils.ReadUShort(data, ref pos);
            res.ThisClass = Utils.ReadUShort(data, ref pos);
            res.SuperClass = Utils.ReadUShort(data, ref pos);

            res.InterfacesCount = Utils.ReadUShort(data, ref pos);
            res.Interfaces = new ushort[res.InterfacesCount];
            for (int i = 0; i < res.InterfacesCount; ++i) {
                res.Interfaces[i] = Utils.ReadUShort(data, ref pos);
            }

            res.FieldsCount = Utils.ReadUShort(data, ref pos);
            res.Fields = new FieldInfo[res.FieldsCount];
            for (int i = 0; i < res.FieldsCount; ++i) {
                res.Fields[i] = FieldInfo.ParseData(res, data, ref pos);
            }

            res.MethodsCount = Utils.ReadUShort(data, ref pos);
            res.Methods = new MethodInfo[res.MethodsCount];
            for (int i = 0; i < res.MethodsCount; ++i) {
                res.Methods[i] = MethodInfo.ParseData(res, data, ref pos);
            }

            res.AttributesCount = Utils.ReadUShort(data, ref pos);
            res.Attributes = new AttributeDescription[res.AttributesCount];
            for (int i = 0; i < res.AttributesCount; ++i) {
                res.Attributes[i] = AttributeDescription.ParseData(data, ref pos);
            }

            res.AttributeParsers = AttributeParser.GenerateAttributeMap(res, res.Attributes);
            return res;
        }
    }
}
