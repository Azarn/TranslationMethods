using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class ConstantPoolDescription {
        public ConstantPoolTag Tag;
        public object Info;

        private ConstantPoolDescription() { }

        public static ConstantPoolDescription ParseData(byte[] data, ref int pos) {
            ConstantPoolDescription res = new ConstantPoolDescription();
            res.Tag = (ConstantPoolTag)data[pos++];
            switch (res.Tag) {
                case ConstantPoolTag.CONSTANT_Class:
                    res.Info = CONSTANT_Class_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Fieldref:
                    res.Info = CONSTANT_GeneralRef_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Methodref:
                    res.Info = CONSTANT_GeneralRef_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_InterfaceMethodref:
                    res.Info = CONSTANT_GeneralRef_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_String:
                    res.Info = CONSTANT_String_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Integer:
                    res.Info = CONSTANT_B4_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Float:
                    res.Info = CONSTANT_B4_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Long:
                    res.Info = CONSTANT_B8_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Double:
                    res.Info = CONSTANT_B8_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_NameAndType:
                    res.Info = CONSTANT_NameAndType_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Utf8:
                    res.Info = CONSTANT_Utf8_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_MethodHandle:
                    res.Info = CONSTANT_MethodHandle_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_MethodType:
                    res.Info = CONSTANT_MethodType_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_InvokeDynamic:
                    res.Info = CONSTANT_InvokeDynamic_info.ParseData(data, ref pos);
                    break;
            }
            return res;
        }
    }

    public class CONSTANT_Class_info {
        public ushort NameIndex;

        private CONSTANT_Class_info() { }

        public static CONSTANT_Class_info ParseData(byte[] data, ref int pos) {
            CONSTANT_Class_info res = new CONSTANT_Class_info();
            res.NameIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }

    public class CONSTANT_GeneralRef_info {
        public ushort ClassIndex;
        public ushort NameAndTypeIndex;

        private CONSTANT_GeneralRef_info() { }

        public static CONSTANT_GeneralRef_info ParseData(byte[] data, ref int pos) {
            CONSTANT_GeneralRef_info res = new CONSTANT_GeneralRef_info();
            res.ClassIndex = Utils.ReadUShort(data, ref pos);
            res.NameAndTypeIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }

    public class CONSTANT_String_info {
        public ushort StringIndex;

        private CONSTANT_String_info() { }

        public static CONSTANT_String_info ParseData(byte[] data, ref int pos) {
            CONSTANT_String_info res = new CONSTANT_String_info();
            res.StringIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }

    public class CONSTANT_B4_info {
        public byte[] Bytes = new byte[4];

        private CONSTANT_B4_info() { }

        public static CONSTANT_B4_info ParseData(byte[] data, ref int pos) {
            CONSTANT_B4_info res = new CONSTANT_B4_info();
            for (int i = 0; i < 4; ++i) {
                res.Bytes[i] = data[pos++];
            }
            return res;
        }
    }

    public class CONSTANT_B8_info {
        public byte[] HighBytes = new byte[4];
        public byte[] LowBytes = new byte[4];

        private CONSTANT_B8_info() { }

        public static CONSTANT_B8_info ParseData(byte[] data, ref int pos) {
            CONSTANT_B8_info res = new CONSTANT_B8_info();
            for (int i = 0; i < 4; ++i) {
                res.HighBytes[i] = data[pos++];
            }
            for (int i = 0; i < 4; ++i) {
                res.LowBytes[i] = data[pos++];
            }
            return res;
        }
    }

    public class CONSTANT_NameAndType_info {
        public ushort NameIndex;
        public ushort DescriptorIndex;

        private CONSTANT_NameAndType_info() { }

        public static CONSTANT_NameAndType_info ParseData(byte[] data, ref int pos) {
            CONSTANT_NameAndType_info res = new CONSTANT_NameAndType_info();
            res.NameIndex = Utils.ReadUShort(data, ref pos);
            res.DescriptorIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }

    public class CONSTANT_Utf8_info {
        public ushort Length;
        public byte[] Bytes;
        public string String;

        private CONSTANT_Utf8_info() { }

        public static string ConvertFromModifiedUTF8(byte[] data) {
            StringBuilder sb = new StringBuilder();
            int pos = 0;

            while (pos < data.Length) {
                byte b = data[pos++];
                char c;
                if ((b & 0x80) == 0) {
                    c = (char)b;
                } else if ((b & 0xE0) == 0xC0) {
                    c = (char)(((b & 0x1f) << 6) + (data[pos++] & 0x3f));
                } else if ((b & 0xF0) == 0xE0) {
                    c = (char)(((b & 0xf) << 12) + ((data[pos++] & 0x3f) << 6) + (data[pos++] & 0x3f));
                } else {
                    throw new Exception("Cannot parse UTF-8 string!");
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static CONSTANT_Utf8_info ParseData(byte[] data, ref int pos) {
            CONSTANT_Utf8_info res = new CONSTANT_Utf8_info();
            res.Length = Utils.ReadUShort(data, ref pos);
            res.Bytes = new byte[res.Length];
            for (int i = 0; i < res.Length; ++i) {
                res.Bytes[i] = data[pos++];
            }
            res.String = ConvertFromModifiedUTF8(res.Bytes);
            return res;
        }

        public override string ToString() {
            return String;
        }
    }

    public class CONSTANT_MethodHandle_info {
        public byte ReferenceKind;
        public ushort ReferenceIndex;

        private CONSTANT_MethodHandle_info() { }

        public static CONSTANT_MethodHandle_info ParseData(byte[] data, ref int pos) {
            CONSTANT_MethodHandle_info res = new CONSTANT_MethodHandle_info();
            res.ReferenceKind = data[pos++];
            res.ReferenceIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }

    public class CONSTANT_MethodType_info {
        public ushort DescriptorIndex;

        private CONSTANT_MethodType_info() { }

        public static CONSTANT_MethodType_info ParseData(byte[] data, ref int pos) {
            CONSTANT_MethodType_info res = new CONSTANT_MethodType_info();
            res.DescriptorIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }

    public class CONSTANT_InvokeDynamic_info {
        public ushort BootstrapMethodAttrIndex;
        public ushort NameAndTypeIndex;

        private CONSTANT_InvokeDynamic_info() { }

        public static CONSTANT_InvokeDynamic_info ParseData(byte[] data, ref int pos) {
            CONSTANT_InvokeDynamic_info res = new CONSTANT_InvokeDynamic_info();
            res.BootstrapMethodAttrIndex = Utils.ReadUShort(data, ref pos);
            res.NameAndTypeIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }
    }
}
