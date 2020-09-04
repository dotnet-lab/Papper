using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis
{
    public static class StructDeclarationSyntaxExtension
    {

        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static StructDeclarationSyntax StructNode(this SyntaxNode node, int index = 0)
        {
            return node.FindNode<StructDeclarationSyntax>(index);
        }


        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static StructDeclarationSyntax StructNode(this SyntaxNode node, string name)
        {
            var nodes = node.FindNodes<StructDeclarationSyntax>();
            foreach (var item in nodes)
            {
                if (item.NameScript() == name)
                {
                    return item;
                }
            }
            return default;
        }

    }
}
