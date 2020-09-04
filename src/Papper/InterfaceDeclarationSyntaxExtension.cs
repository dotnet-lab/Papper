using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis
{
    public static class InterfaceDeclarationSyntaxExtension
    {

        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax InterfaceNode(this SyntaxNode node, int index = 0)
        {
            return node.FindNode<InterfaceDeclarationSyntax>(index);
        }


        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static InterfaceDeclarationSyntax InterfaceNode(this SyntaxNode node, string name)
        {
            var nodes = node.FindNodes<InterfaceDeclarationSyntax>();
            foreach (var item in nodes)
            {
                if (item.Name() == name)
                {
                    return item;
                }
            }
            return default;
        }

    }
}
