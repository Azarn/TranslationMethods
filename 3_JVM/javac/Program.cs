using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace javac {
    class Program {
        static void Main(string[] args) {
            string filename = @"..\..\NOD.java";

            AntlrInputStream inputStream = new AntlrFileStream(filename);
            var javaLexer = new JavaLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(javaLexer);
            var javaParser = new JavaParser(commonTokenStream);

            var visitor = new JavaVisitor(new StackFrame());
            visitor.Visit(javaParser.compileUnit());
            var stackFrame = visitor.StackFrame;
        }
    }
}
