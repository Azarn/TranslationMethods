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
        static void Main(string[] args) {
            byte[] data = File.ReadAllBytes(@"..\..\NOD.class");
            ClassFile cFile = ClassFile.ParseClassFile(data);
        }
    }
}
