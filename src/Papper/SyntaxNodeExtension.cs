using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.CodeAnalysis
{
    public static class SyntaxNodeExtension
    {

        public static IEnumerable<TNode> FindNodes<TNode>(this SyntaxNode node)
        {

            return (from declarationSyntax
                          in node.DescendantNodes().OfType<TNode>()
                    select declarationSyntax);

        }


        public static TNode FindNode<TNode>(this SyntaxNode node, int index)
        {

            var result = FindNodes<TNode>(node);
            return result != default ? result.ToArray()[index] : default;

        }


        public static string FullScript(this SyntaxNode node)
        {
            return node == default ? default : node.ToFullString();
        }


        public static string NameScript(this BaseTypeDeclarationSyntax node)
        {
            return node == default ? default : node.Identifier.Text;
        }


        public static string ModifiersScript(this MemberDeclarationSyntax node)
        {
            return node == default ? default : node.Modifiers.ToFullString();
        }


        public static IEnumerable<string> BaseListNames(this BaseTypeDeclarationSyntax node)
        {
            return node == default ? default : node.BaseList.ChildNodes().Select(item => item.ToFullString());
        }


        public static string BaseListScript(this BaseTypeDeclarationSyntax node)
        {
            return node == default ? default : node.BaseList.ToFullString();
        }


        public static string DeclarationText(this TypeDeclarationSyntax node)
        {
            return node != default ? node.Modifiers.ToFullString() + node.Identifier.ToFullString() + node.BaseList.ToFullString() : default;
        }
    }
}
