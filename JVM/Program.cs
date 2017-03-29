using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JVM.ClassDescription;
using JVM.OpCodes;

namespace JVM {
    class Program {
        public static string MAIN_NAME = "main";
        public static string MAIN_SIGNATURE = "([Ljava/lang/String;)V";

        public static void RunMethod(MethodInfo m) {
            var codeAttr = m.AttributeParsers["Code"] as CodeAttributeParser;
            if (codeAttr == null) {
                throw new Exception("Code attribute is not found!");
            }
        }

        static void Main(string[] args) {
            byte[] data = File.ReadAllBytes(@"..\..\NOD.class");
            ClassFile cFile = ClassFile.ParseClassFile(data);

            // TODO: check cFile.AccessFlags

            MethodInfo methodMain = null;
            foreach(var m in cFile.Methods) {
                if (m.AccessFlags != (short)(MethodAccessFlags.ACC_PUBLIC | MethodAccessFlags.ACC_STATIC)) {
                    continue;
                }
                var mName = (CONSTANT_Utf8_info)cFile.GetConstant(m.NameIndex).Info;
                if (!mName.String.Equals(MAIN_NAME)) {
                    continue;
                }

                var mSignature = (CONSTANT_Utf8_info)cFile.GetConstant(m.DescriptorIndex).Info;
                if (!mSignature.String.Equals(MAIN_SIGNATURE)) {
                    continue;
                }

                methodMain = m;
                break;
            }

            if (methodMain == null) {
                throw new Exception("Main method is not found!");
            }
            RunMethod(methodMain);
        }
    }
}
