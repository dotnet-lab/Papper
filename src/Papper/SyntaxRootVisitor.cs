using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class SyntaxRootVisitor
{

    public SyntaxNode Root;

    private SyntaxRootVisitor(SyntaxNode node)
    {
        Root = node.SyntaxTree.GetRoot();
    }
    private SyntaxRootVisitor(string code)
    {
        Root = CSharpSyntaxTree.ParseText(SourceText.From(code, Encoding.UTF8)).GetRoot();
    }

    public static SyntaxRootVisitor CreateFrom(SyntaxNode node)
    {
        return new SyntaxRootVisitor(node);
    }
    public static SyntaxRootVisitor CreateFrom(string code)
    {
        return new SyntaxRootVisitor(code);
    }



    /// <summary>
    /// 获取Using 节点集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<UsingDirectiveSyntax> UsingNodes()
    {
        return Root.FindNodes<UsingDirectiveSyntax>();
    }




    /// <summary>
    /// 获取Using Name集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> UsingNames()
    {
        return from usingDirectiveSyntax
               in Root.DescendantNodes().OfType<UsingDirectiveSyntax>()
               select usingDirectiveSyntax.Name.ToString();
    }




    /// <summary>
    /// 获取 Using 全代码集合
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> UsingFullScripts()
    {
        return from usingDirectiveSyntax
               in Root.DescendantNodes().OfType<UsingDirectiveSyntax>()
               select usingDirectiveSyntax.ToFullString();
    }




    /// <summary>
    /// 获取命名空间所有节点
    /// </summary>
    /// <returns></returns>
    public IEnumerable<NamespaceDeclarationSyntax> NamespaceNodes()
    {

        return Root.FindNodes<NamespaceDeclarationSyntax>();

    }

    /// <summary>
    /// 根据命名空间的顺序获取命名空间节点，默认获取第一个
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public NamespaceDeclarationSyntax NamespaceNode(int index = 0)
    {

        var nodes = NamespaceNodes();
        if (nodes.Count() != 0)
        {
            return nodes.ToArray()[index];
        }
        return default;

    }

    public SyntaxNode GetAvailiableNode()
    {
        SyntaxNode node = NamespaceNode(0);
        if (node == default)
        {
            node = Root;
        }
        return node;
    }

    /// <summary>
    /// 获取命名空间名称
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string NamespaceName(int index = 0)
    {

        var nodes = NamespaceNode(index);
        if (nodes != default)
        {
            return nodes.Name.ToString();
        }
        return default;

    }

}






