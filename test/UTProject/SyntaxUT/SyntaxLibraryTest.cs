using Microsoft.CodeAnalysis;
using System;
using System.Linq;
using Xunit;


namespace NatashaUT
{

    [Trait("语法辅助库测试", "")]
    public class SyntaxLibraryTest
    {


        [Fact(DisplayName = "空引用测试")]
        public void T1()
        {

            //ScriptCompiler.Init();
            string text = @"
namespace HelloWorld
{
}";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            Assert.Equal(0, visitor.UsingNames().Count());

        }




        [Fact(DisplayName = "引用测试")]
        public void T2()
        {

            //ScriptCompiler.Init();
            string text = @"using System;
namespace HelloWorld
{
}";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            Assert.Equal("System", visitor.UsingNames().First());
            Assert.Equal($"using System;{Environment.NewLine}", visitor.UsingFullScripts().First());
        }




        [Fact(DisplayName = "空命名空间测试")]
        public void T3()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
public class HelloWorld
{
}";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            Assert.Equal(default, visitor.NamespaceName());
        }




        [Fact(DisplayName = "命名空间测试")]
        public void T4()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
namespace HelloWorld
{
}namespace HelloWorld1
{
}";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            Assert.Equal("HelloWorld", visitor.NamespaceName());
            Assert.Equal("HelloWorld1", visitor.NamespaceName(1));
        }


        [Fact(DisplayName = "对象定义测试")]
        public void T5()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
namespace HelloWorld
{
    public enum Test1{}
    public class Test2{ public string Age;}
public interface Test3{}
public struct Test4{}
}namespace HelloWorld1
{
public enum Test5{}
    public class Test6{}
public interface Test7{}
public struct Test8{}
}";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            Assert.Equal("Test2", visitor.NamespaceNode(0).ClassNode(0).NameScript());
            Assert.Equal($"public interface Test7{{}}", visitor.NamespaceNode(1).InterfaceNode(0).FullScript().Trim());
            Assert.Equal("Test1", visitor.NamespaceNode(0).EnumNode(0).NameScript());
            Assert.Equal($"public enum Test5{{}}", visitor.NamespaceNode(1).EnumNode(0).FullScript().Trim());
        }

        [Fact(DisplayName = "实现列表测试")]
        public void TI()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
public class HelloWorld : A,B
{
}";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            Assert.NotNull(visitor.Root.ClassNode(0).BaseListNames());
            Assert.Equal("A", visitor.Root.ClassNode(0).BaseListNames().First());
            Assert.Equal("B", visitor.Root.ClassNode(0).BaseListNames().ToArray()[1].Trim());

        }

        [Fact(DisplayName = "字段定义测试")]
        public void T6()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
public class A
{
    public string Name;
    public int Age;
    public DateTime Time;
    public int[] Ages;
    public IList<int> Agees;
}
";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            var node = visitor.Root.ClassNode(0);
            var fieldNames = FieldDeclarationSyntaxExtension.Names(node).ToArray();
            var fieldScripts = FieldDeclarationSyntaxExtension.FullScripts(node).ToArray();
            Assert.Equal("Name", fieldNames[0]);
            Assert.Equal("public string Name;", fieldScripts[0].Trim());
            Assert.Equal("Age", fieldNames[1]);
            Assert.Equal("public int Age;", fieldScripts[1].Trim());
            Assert.Equal("Time", fieldNames[2]);
            Assert.Equal("public DateTime Time;", fieldScripts[2].Trim());
            Assert.Equal("Ages", fieldNames[3]);
            Assert.Equal("public int[] Ages;", fieldScripts[3].Trim());
            Assert.Equal("Agees", fieldNames[4]);
            Assert.Equal("public IList<int> Agees;", fieldScripts[4].Trim());

        }

        //aa 
        [Fact(DisplayName = "属性定义测试")]
        public void T7()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
public class A
{
    public string Name{get;set;}
    public int Age{get{return 1;}}
    public DateTime Time{set{Name=value.ToString();}}
}
";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            var node = visitor.Root.ClassNode(0);
            var names = PropertyDeclarationSyntaxExtension.Names(node).ToArray();
            var scripts = PropertyDeclarationSyntaxExtension.FullScripts(node).ToArray();

            Assert.Equal("Name", names[0]);
            Assert.Equal("Age", names[1]);
            Assert.Equal("Time", names[2]);

            //full
            Assert.Equal("public string Name{get;set;}", scripts[0].Trim());
            //get
            var pnode= node.PropertyNode("Age");
            Assert.Equal("return 1;", pnode.GetGetText());
            Assert.Equal("    public int Age", pnode.DeclarationText());
            //set
            var tnode = node.PropertyNode("Time");
            Assert.Equal("Name=value.ToString();", tnode.GetSetText());

        }

        [Fact(DisplayName = "方法定义测试")]
        public void T8()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
public class A
{
    public string Name(){return ""a"";}
    public int Age(){return 1;}
    public DateTime Time(){ Name=value.ToString();}
}
";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            var node = visitor.GetAvailiableNode().ClassNode();
            var names = node.MethodNames().ToArray();
            var scripts = node.MethodFullScripts().ToArray();

            Assert.Equal("Name", names[0]);
            Assert.Equal("Age", names[1]);
            Assert.Equal("Time", names[2]);

            Assert.Equal("public string Name(){return \"a\";}", scripts[0].Trim());
            Assert.Equal("return 1;", node.MethodNode("Age").ReturnExpression());
            Assert.Equal("Name=value.ToString();", node.MethodNode("Time").Body());

        }

        [Fact(DisplayName = "方法定义测试1")]
        public void T9()
        {

            //ScriptCompiler.Init();
            string text = @"
using System;
public class A
{

    [A]
    [B]
    public string Name(ref int a ,ref string b){return ""a"";}
    public ref int Age(){return 1;}
}
";
            var visitor = SyntaxRootVisitor.CreateFrom(text);
            var node = visitor.GetAvailiableNode().ClassNode();
            var names = node.MethodNames().ToArray();

            Assert.Equal("Name", names[0]);
            Assert.Equal("Age", names[1]);

            Assert.Equal("ref int ", node.MethodNode("Age").ReturnType());
            Assert.Equal("return 1;", node.MethodNode("Age").Body());

            var nameNode = node.MethodNode("Name");

            Assert.Equal("ref int ,ref string ", string.Join(",", nameNode.ParametersType()));
            Assert.Equal("int ,string ", string.Join(",", nameNode.ParametersType(false)));


            Assert.Equal($"{Environment.NewLine}    [A]{Environment.NewLine}    [B]{Environment.NewLine}", nameNode.AttributeFullScript());
            
            Assert.Equal("    public string Name(ref int a ,ref string b)", nameNode.DeclarationText());
            
            Assert.Equal("a,b", string.Join(",", nameNode.ParametersName()));
            Assert.Equal("ref int a ,ref string b", string.Join(",", nameNode.Parameters()));
            Assert.Equal("return \"a\";", string.Join(",", nameNode.ReturnExpression()));


        }

        //        [Fact(DisplayName = "动态类生成测试2")]
        //        public void RunClassName4()
        //        {

        //            //ScriptCompiler.Init();
        //            string text = @"
        //namespace HelloWorld
        //{public class Test{public Test(){
        //            Name=""111"";
        //        }public string Name;
        //        public int Age{get;set;}
        //    }
        //}";
        //            //根据脚本创建动态类
        //            var oop = new NAssembly();
        //            oop.AssemblyBuilder.Compiler.Domain = DomainManagement.Random;
        //            oop.AddScript(text);
        //            Type type = oop.GetTypeFromShortName("Test");
        //            Assert.Equal("Test", type.Name);
        //        }




        //        [Fact(DisplayName = "选择类")]
        //        public void RunClassName1()
        //        {

        //            //ScriptCompiler.Init();
        //            string text = @"using System;
        //using System.Collections;
        //using System.Linq;
        //using System.Text;

        //namespace HelloWorld
        //{
        //    public class TestIndex1
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }
        //    public class TestIndex2
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }

        //    public class TestIndex3
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }
        //}

        //namespace HelloWorld{
        //    public struct TestStruct1{}
        //    public struct TestStruct2{}
        //    public class TestIndex4
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }
        //}

        //";
        //            //根据脚本创建动态类
        //            AssemblyCSharpBuilder oop = new AssemblyCSharpBuilder();
        //            oop.Compiler.Domain = DomainManagement.Random;
        //            oop.Add(text);
        //            Type type = oop.GetTypeFromShortName("TestIndex3");
        //            Assert.Equal("TestIndex3", type.Name);
        //        }




        //        [Fact(DisplayName = "选择命名空间+类")]
        //        public void RunClassName2()
        //        {
        //            //ScriptCompiler.Init();
        //            string text = @"using System;
        //using System.Collections;
        //using System.Linq;
        //using System.Text;
        //using System.Collections.Generic;
        //namespace HelloWorld
        //{
        //    public class TestIndex1
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }
        //    public class TestIndex2
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //        public List<object> ooo;
        //    }

        //    public class TestIndex3
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }
        //}

        //namespace HelloWorld{

        //    public struct TestStruct1{}
        //    public struct TestStruct2{}
        //    public class TestIndex4
        //    {
        //        public string Name;
        //        public int Age{get;set;}
        //    }
        //}

        //";
        //            //根据脚本创建动态类
        //            Assert.Equal("TestIndex2", ScriptHelper.GetClassName(text, 1, 2));
        //            Assert.Equal("TestIndex4", ScriptHelper.GetClassName(text, 2, 1));
        //            Assert.Equal("TestStruct2", ScriptHelper.GetStructName(text, 2, 2));
        //        }


        //        [Fact(DisplayName = "字符串格式化测试1")]
        //        public void RunClassName5()
        //        {

        //            var content = @"unsafe class C
        //{
        //    delegate * < int,  int> functionPointer;
        //}";

        //            var expected = @"unsafe class C
        //{
        //    delegate*<int, int> functionPointer;
        //}";


        //            NatashaCSharpSyntax syntax = new NatashaCSharpSyntax();
        //            syntax.AddTreeToCache(content);
        //            var result = syntax.TreeCache[expected].ToString();
        //            Assert.Equal(expected, result);
        //        }
        //        [Fact(DisplayName = "字符串格式化测试2")]
        //        public void RunClassName6()
        //        {

        //            var content = @"class A
        //            {        
        //int             i               =               20          ;           int             j           =           1           +           2       ;
        //                        T           .               S           =           Test            (           10              )           ;
        //                        }";

        //            var expected = @"class A
        //{
        //    int i = 20; int j = 1 + 2;
        //    T.S           =           Test(           10              );
        //}";

        //            NatashaCSharpSyntax syntax = new NatashaCSharpSyntax();
        //            syntax.AddTreeToCache(content);
        //            var result = syntax.TreeCache[expected].ToString();
        //            Assert.Equal(expected, result);
        //        }




        //        //        [Fact(DisplayName = "字符串格式化测试3")]
        //        //        public void RunClassName7()
        //        //        {

        //        //            var initial =
        //        //@"using A = B;
        //        //using C;
        //        //using D = E;
        //        //using F;";

        //        //            var final =
        //        //    @"using C;
        //        //using F;
        //        //using A = B;
        //        //using D = E;
        //        //";

        //        //            NatashaCSharpSyntax syntax = new NatashaCSharpSyntax();
        //        //            syntax.AddTreeToCache(initial);
        //        //            var result = syntax.TreeCache[final].ToString();
        //        //            Assert.Equal(final, result);
        //        //        }


        //        [Fact(DisplayName = "字符串格式化测试4")]
        //        public void RunClassName8()
        //        {

        //            var initial = "int i=0 ; var t=new{Name=\"\"};";

        //            var final = "int i = 0; var t = new { Name = \"\" };";

        //            NatashaCSharpSyntax syntax = new NatashaCSharpSyntax();
        //            syntax.AddTreeToCache(initial);
        //            var result = syntax.TreeCache[final].ToString();
        //            Assert.Equal(final, result);
        //        }


        //        [Fact(DisplayName = "Release测试")]
        //        public void ReleaseTest()
        //        {

        //            var script = "NormalTestModel result = new NormalTestModel();return result;";
        //            NDelegate.RandomDomain(item => item.UseFileCompile()).Func<NormalTestModel>(script)();
        //            Assert.Equal(0, 0);
        //        }
    }
}
