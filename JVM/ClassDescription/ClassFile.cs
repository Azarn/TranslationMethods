using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class ClassFile {
        public byte[] Magic = new byte[4];
        public short MinorVersion;
        public short MajorVersion;
        public short ConstantPoolCount;
        public ConstantPoolDescription[] ConstantPool;
        public short AccessFlags;
        public short ThisClass;
        public short SuperClass;
        public short InterfacesCount;
        public short[] Interfaces;
        public short FieldsCount;
        public FieldInfo[] Fields;
        public short MethodsCount;
        public MethodInfo[] Methods;
        public short AttributesCount;
        public AttributeDescription[] Attributes;

        private ClassFile() { }

        public static ClassFile ParseClassFile(byte[] data) {
            ClassFile res = new ClassFile();
            int pos = 0;
            for (int i = 0; i < 4; ++i) {
                res.Magic[i] = data[pos++];
            }

            res.MinorVersion = Utils.ReadShort(data, ref pos);
            res.MajorVersion = Utils.ReadShort(data, ref pos);

            res.ConstantPoolCount = Utils.ReadShort(data, ref pos);
            res.ConstantPool = new ConstantPoolDescription[res.ConstantPoolCount - 1];
            for (int i = 0; i < res.ConstantPoolCount - 1; ++i) {
                res.ConstantPool[i] = ConstantPoolDescription.ParseData(data, ref pos);
            }

            res.AccessFlags = Utils.ReadShort(data, ref pos);
            res.ThisClass = Utils.ReadShort(data, ref pos);
            res.SuperClass = Utils.ReadShort(data, ref pos);

            res.InterfacesCount = Utils.ReadShort(data, ref pos);
            res.Interfaces = new short[res.InterfacesCount];
            for (int i = 0; i < res.InterfacesCount; ++i) {
                res.Interfaces[i] = Utils.ReadShort(data, ref pos);
            }

            res.FieldsCount = Utils.ReadShort(data, ref pos);
            res.Fields = new FieldInfo[res.FieldsCount];
            for (int i = 0; i < res.FieldsCount; ++i) {
                res.Fields[i] = FieldInfo.ParseData(data, ref pos);
            }

            res.MethodsCount = Utils.ReadShort(data, ref pos);
            res.Methods = new MethodInfo[res.MethodsCount];
            for (int i = 0; i < res.MethodsCount; ++i) {
                res.Methods[i] = MethodInfo.ParseData(data, ref pos);
            }

            res.AttributesCount = Utils.ReadShort(data, ref pos);
            res.Attributes = new AttributeDescription[res.AttributesCount];
            for (int i = 0; i < res.AttributesCount; ++i) {
                res.Attributes[i] = AttributeDescription.ParseData(data, ref pos);
            }
            return res;
        }
    }
}
