using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVM.ClassDescription {
    public class ConstantPoolDescription {
        public ConstantPoolTag Tag;
        public ByteSerializable Info;

        public ConstantPoolDescription(ConstantPoolTag tag, ByteSerializable info) {
            Tag = tag;
            Info = info;
        }

        private ConstantPoolDescription() {}

        public static ConstantPoolDescription ParseData(byte[] data, ref int pos) {
            ConstantPoolDescription res = new ConstantPoolDescription();
            res.Tag = (ConstantPoolTag)data[pos++];
            switch (res.Tag) {
                case ConstantPoolTag.CONSTANT_Class:
                    res.Info = CONSTANT_Class_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Fieldref:
                case ConstantPoolTag.CONSTANT_Methodref:
                case ConstantPoolTag.CONSTANT_InterfaceMethodref:
                    res.Info = CONSTANT_GeneralRef_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_String:
                    res.Info = CONSTANT_String_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Integer:
                case ConstantPoolTag.CONSTANT_Float:
                    res.Info = CONSTANT_B4_info.ParseData(data, ref pos);
                    break;
                case ConstantPoolTag.CONSTANT_Long:
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

        public byte[] BuildData() {
            var res = new List<byte>();
            res.Add((byte)Tag);
            res.AddRange(Info.BuildData());
            return res.ToArray();
        }
    }

    public interface ByteSerializable {
        byte[] BuildData();
    }

    public class CONSTANT_Class_info : ByteSerializable {
        public ushort NameIndex;

        private CONSTANT_Class_info() { }

        public CONSTANT_Class_info(ushort nameIndex) {
            NameIndex = nameIndex;
        }

        public static CONSTANT_Class_info ParseData(byte[] data, ref int pos) {
            CONSTANT_Class_info res = new CONSTANT_Class_info();
            res.NameIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(NameIndex);
        }
    }

    public class CONSTANT_GeneralRef_info : ByteSerializable {
        public ushort ClassIndex;
        public ushort NameAndTypeIndex;

        private CONSTANT_GeneralRef_info() { }

        public CONSTANT_GeneralRef_info(ushort classIndex, ushort nameAndTypeIndex) {
            ClassIndex = classIndex;
            NameAndTypeIndex = nameAndTypeIndex;
        }

        public static CONSTANT_GeneralRef_info ParseData(byte[] data, ref int pos) {
            CONSTANT_GeneralRef_info res = new CONSTANT_GeneralRef_info();
            res.ClassIndex = Utils.ReadUShort(data, ref pos);
            res.NameAndTypeIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(ClassIndex).Concat(Utils.WriteUShort(NameAndTypeIndex)).ToArray();
        }
    }

    public class CONSTANT_String_info : ByteSerializable {
        public ushort StringIndex;

        private CONSTANT_String_info() { }

        public static CONSTANT_String_info ParseData(byte[] data, ref int pos) {
            CONSTANT_String_info res = new CONSTANT_String_info();
            res.StringIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(StringIndex);
        }
    }

    public class CONSTANT_B4_info : ByteSerializable {
        public byte[] Bytes = new byte[4];

        public CONSTANT_B4_info(byte[] bytes) {
            Bytes = bytes;
        }

        private CONSTANT_B4_info() { }

        public static CONSTANT_B4_info ParseData(byte[] data, ref int pos) {
            CONSTANT_B4_info res = new CONSTANT_B4_info();
            for (int i = 0; i < 4; ++i) {
                res.Bytes[i] = data[pos++];
            }
            Utils.ReverseIfEndian(res.Bytes);
            return res;
        }

        public int ToInt() {
            return BitConverter.ToInt32(Bytes, 0);
        }

        public float ToFloat() {
            return BitConverter.ToSingle(Bytes, 0);
        }

        public byte[] BuildData() {
            var res = (byte[])Bytes.Clone();
            Utils.ReverseIfEndian(res);
            return res;
        }
    }

    public class CONSTANT_B8_info : ByteSerializable {
        public byte[] HighBytes = new byte[4];
        public byte[] LowBytes = new byte[4];

        public CONSTANT_B8_info(byte[] highBytes, byte[] lowBytes) {
            HighBytes = highBytes;
            LowBytes = lowBytes;
        }

        private CONSTANT_B8_info() { }

        public static CONSTANT_B8_info ParseData(byte[] data, ref int pos) {
            CONSTANT_B8_info res = new CONSTANT_B8_info();
            for (int i = 0; i < 4; ++i) {
                res.HighBytes[i] = data[pos++];
            }
            Utils.ReverseIfEndian(res.HighBytes);
            for (int i = 0; i < 4; ++i) {
                res.LowBytes[i] = data[pos++];
            }
            Utils.ReverseIfEndian(res.LowBytes);
            return res;
        }

        public long ToLong() {
            return (long)(((ulong)BitConverter.ToUInt32(HighBytes, 0) << 32) + BitConverter.ToUInt32(LowBytes, 0));
        }

        public double ToDouble() {
            return BitConverter.Int64BitsToDouble(ToLong());
        }

        public byte[] BuildData() {
            var high = (byte[])HighBytes.Clone();
            var low = (byte[])LowBytes.Clone();
            Utils.ReverseIfEndian(high);
            Utils.ReverseIfEndian(low);
            return high.Concat(low).ToArray();
        }
    }

    public class CONSTANT_NameAndType_info : ByteSerializable {
        public ushort NameIndex;
        public ushort DescriptorIndex;

        private CONSTANT_NameAndType_info() { }

        public CONSTANT_NameAndType_info(ushort nameIndex, ushort descriptorIndex) {
            NameIndex = nameIndex;
            DescriptorIndex = descriptorIndex;
        }

        public static CONSTANT_NameAndType_info ParseData(byte[] data, ref int pos) {
            CONSTANT_NameAndType_info res = new CONSTANT_NameAndType_info();
            res.NameIndex = Utils.ReadUShort(data, ref pos);
            res.DescriptorIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(NameIndex).Concat(Utils.WriteUShort(DescriptorIndex)).ToArray();
        }
    }

    public class CONSTANT_Utf8_info : ByteSerializable {
        public ushort Length;
        public byte[] Bytes;
        public string String => _string;

        private string _string;

        private CONSTANT_Utf8_info() { }

        public CONSTANT_Utf8_info(string s) {
            _string = s;
            Bytes = ConvertToModifiedUTF8(s);
            Length = (ushort)Bytes.Length;
        }

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

        public static byte[] ConvertToModifiedUTF8(string s) {
            var res = new List<byte>();
            foreach(var c in s) {
                if (c >= '\u0001' && c <= '\u007F') {
                    res.Add((byte)c);
                } else if (c == '\u0000' || c <= '\u07FF') {
                    res.Add((byte)(((c & 0x7C0) >> 6) | 0xC0));
                    res.Add((byte)((c & 0x3F) | 0x80));
                } else if (c <= '\uFFFF') {
                    res.Add((byte)(((c & 0xF000) >> 12) | 0xE0));
                    res.Add((byte)(((c & 0xFC0) >> 6) | 0x80));
                    res.Add((byte)((c & 0x3F) | 0x80));
                } else {
                    throw new Exception("Cannot convert UTF-8 string to bytes!");
                }
            }
            return res.ToArray();
        }

        public static CONSTANT_Utf8_info ParseData(byte[] data, ref int pos) {
            CONSTANT_Utf8_info res = new CONSTANT_Utf8_info();
            res.Length = Utils.ReadUShort(data, ref pos);
            res.Bytes = new byte[res.Length];
            for (int i = 0; i < res.Length; ++i) {
                res.Bytes[i] = data[pos++];
            }
            res._string = ConvertFromModifiedUTF8(res.Bytes);
            return res;
        }

        public override string ToString() {
            return String;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(Length).Concat(Bytes).ToArray();
        }
    }

    public class CONSTANT_MethodHandle_info : ByteSerializable {
        public byte ReferenceKind;
        public ushort ReferenceIndex;

        private CONSTANT_MethodHandle_info() { }

        public static CONSTANT_MethodHandle_info ParseData(byte[] data, ref int pos) {
            CONSTANT_MethodHandle_info res = new CONSTANT_MethodHandle_info();
            res.ReferenceKind = data[pos++];
            res.ReferenceIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return new byte[1] { ReferenceKind }.Concat(Utils.WriteUShort(ReferenceIndex)).ToArray();
        }
    }

    public class CONSTANT_MethodType_info : ByteSerializable {
        public ushort DescriptorIndex;

        private CONSTANT_MethodType_info() { }

        public static CONSTANT_MethodType_info ParseData(byte[] data, ref int pos) {
            CONSTANT_MethodType_info res = new CONSTANT_MethodType_info();
            res.DescriptorIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(DescriptorIndex);
        }
    }

    public class CONSTANT_InvokeDynamic_info : ByteSerializable {
        public ushort BootstrapMethodAttrIndex;
        public ushort NameAndTypeIndex;

        private CONSTANT_InvokeDynamic_info() { }

        public static CONSTANT_InvokeDynamic_info ParseData(byte[] data, ref int pos) {
            CONSTANT_InvokeDynamic_info res = new CONSTANT_InvokeDynamic_info();
            res.BootstrapMethodAttrIndex = Utils.ReadUShort(data, ref pos);
            res.NameAndTypeIndex = Utils.ReadUShort(data, ref pos);
            return res;
        }

        public byte[] BuildData() {
            return Utils.WriteUShort(BootstrapMethodAttrIndex).Concat(Utils.WriteUShort(NameAndTypeIndex)).ToArray();
        }
    }
}
