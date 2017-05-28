using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class ClassFile {
        public byte[] Magic = new byte[4] { 0xCA, 0xFE, 0xBA, 0xBE };
        public ushort MinorVersion;
        public ushort MajorVersion;
        public ushort ConstantPoolCount = 0;
        public ConstantPoolDescription[] ConstantPool = new ConstantPoolDescription[0];
        public ushort AccessFlags;
        public ushort ThisClass;
        public ushort SuperClass = 0;
        public ushort InterfacesCount = 0;
        public ushort[] Interfaces = new ushort[0];
        public ushort FieldsCount = 0;
        public FieldInfo[] Fields = new FieldInfo[0];
        public ushort MethodsCount = 0;
        public MethodInfo[] Methods = new MethodInfo[0];
        public ushort AttributesCount = 0;
        public AttributeDescription[] Attributes = new AttributeDescription[0];

        public IDictionary<string, object> AttributeParsers;

        private IDictionary<int, ConstantPoolDescription> INDEX_TO_CONST_MAP = new Dictionary<int, ConstantPoolDescription>();

        private ClassFile() { }

        public ConstantPoolDescription GetConstant(ushort index) {
            return INDEX_TO_CONST_MAP[index - 1];
        }

        private static ConstantPoolDescription[] CalcConstantPool(List<ConstantPoolDescription> constantPool) {
            int size = constantPool.Count;
            foreach(var constant in constantPool) {
                if(constant.Tag == ConstantPoolTag.CONSTANT_Long || constant.Tag == ConstantPoolTag.CONSTANT_Double) {
                    ++size;
                }
            }
            var res = new ConstantPoolDescription[size];
            int i = 0;
            foreach (var constant in constantPool) {
                res[i++] = constant;
                if (constant.Tag == ConstantPoolTag.CONSTANT_Long || constant.Tag == ConstantPoolTag.CONSTANT_Double) {
                    ++i;
                }
            }
            return res;
        }

        public static ClassFile CreateClassFile(ushort minorVersion, ushort majorVersion, ClassAccessFlags accessFlags,
            ushort thisClass, List<ConstantPoolDescription> constantPool, List<MethodInfo> methods) {
            var res = new ClassFile();
            res.MinorVersion = minorVersion;
            res.MajorVersion = majorVersion;
            res.ConstantPool = CalcConstantPool(constantPool);
            res.ConstantPoolCount = (ushort)(res.ConstantPool.Length + 1);
            res.AccessFlags = (ushort)accessFlags;
            res.ThisClass = thisClass;
            res.MethodsCount = (ushort)methods.Count;
            res.Methods = methods.ToArray();
            return res;
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
                ConstantPoolDescription cpd = ConstantPoolDescription.ParseData(data, ref pos);
                res.ConstantPool[i] = cpd;
                res.INDEX_TO_CONST_MAP.Add(i, cpd);
                if (cpd.Tag == ConstantPoolTag.CONSTANT_Double || cpd.Tag == ConstantPoolTag.CONSTANT_Long) {;
                    ++i;                    // fucking java
                }
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

        public byte[] BuildClassFile() {
            var res = new List<byte>();
            res.AddRange(Magic);
            res.AddRange(Utils.WriteUShort(MinorVersion));
            res.AddRange(Utils.WriteUShort(MajorVersion));

            res.AddRange(Utils.WriteUShort(ConstantPoolCount));
            for(int i = 0; i < ConstantPoolCount - 1; ++i) {
                var cpd = ConstantPool[i];
                res.AddRange(cpd.BuildData());
                if (cpd.Tag == ConstantPoolTag.CONSTANT_Double || cpd.Tag == ConstantPoolTag.CONSTANT_Long) {
                    ++i;                    // fucking java
                }
            }

            res.AddRange(Utils.WriteUShort(AccessFlags));
            res.AddRange(Utils.WriteUShort(ThisClass));
            res.AddRange(Utils.WriteUShort(SuperClass));

            res.AddRange(Utils.WriteUShort(InterfacesCount));
            foreach(var @interface in Interfaces) {
                res.AddRange(Utils.WriteUShort(@interface));
            }

            res.AddRange(Utils.WriteUShort(FieldsCount));
            foreach(var field in Fields) {
                res.AddRange(field.BuildData());
            }

            res.AddRange(Utils.WriteUShort(MethodsCount));
            foreach(var method in Methods) {
                res.AddRange(method.BuildData());
            }

            res.AddRange(Utils.WriteUShort(AttributesCount));
            foreach(var attribute in Attributes) {
                res.AddRange(attribute.BuildData());
            }
            return res.ToArray();
        }
    }
}
