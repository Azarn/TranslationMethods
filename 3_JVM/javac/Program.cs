using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JVM;
using JVM.ClassDescription;

namespace javac {
    class Program {
        static void Main(string[] args) {
            string in_filename = @"..\..\NOD.java";
            string out_filename = @"..\..\NOD.class";

            AntlrInputStream inputStream = new AntlrFileStream(in_filename);
            var javaLexer = new JavaLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(javaLexer);
            var javaParser = new JavaParser(commonTokenStream);

            var visitor = new JavaVisitor(new StackFrame());
            visitor.Visit(javaParser.compileUnit());
            var stackFrame = visitor.StackFrame;

            var initInd = stackFrame.GetOrAddMethodref("<init>", "()V", "java/lang/Object");
            var methods = new List<MethodInfo>();
            var initMethod = new MethodInfo(MethodAccessFlags.ACC_PUBLIC,
                stackFrame.GetOrAddString("<init>"),
                stackFrame.GetOrAddString("()V"));

            var initCodeInstructions = new List<byte>();
            initCodeInstructions.Add(0x2A);
            initCodeInstructions.Add(0xB7);
            initCodeInstructions.AddRange(Utils.WriteUShort(initInd));
            initCodeInstructions.Add(0xB1);

            var initCodeAttribute = new CodeAttributeParser(1, 1, initCodeInstructions);
            var initData = initCodeAttribute.BuildData();
            initMethod.AttributesCount = 1;
            initMethod.Attributes = new AttributeDescription[1] {
                new AttributeDescription(stackFrame.GetOrAddString("Code"), (uint)initData.Length, initData)
            };
            methods.Add(initMethod);

            var mainMethod = new MethodInfo(MethodAccessFlags.ACC_PUBLIC | MethodAccessFlags.ACC_STATIC,
                stackFrame.GetOrAddString("main"),
                stackFrame.GetOrAddString("([Ljava/lang/String;)V"));
            var mainCodeAttribute = new CodeAttributeParser(stackFrame.maxStackSize,
                stackFrame.maxLocalsSize, stackFrame.BuildInstructions());
            var mainData = mainCodeAttribute.BuildData();
            mainMethod.AttributesCount = 1;
            mainMethod.Attributes = new AttributeDescription[1] {
                new AttributeDescription(stackFrame.GetOrAddString("Code"), (uint)mainData.Length, mainData)
            };
            methods.Add(mainMethod);
            
            var classFile = ClassFile.CreateClassFile(0, 52, ClassAccessFlags.ACC_PUBLIC | ClassAccessFlags.ACC_SUPER,
                stackFrame.thisClass, stackFrame.constantPool, methods);
            classFile.SuperClass = stackFrame.GetOrAddClass("java/lang/Object");
            File.WriteAllBytes(out_filename, classFile.BuildClassFile());
        }
    }
}
