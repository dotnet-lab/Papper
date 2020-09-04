using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis
{
    public static class EnumDeclarationSyntaxExtension
    {

        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static EnumDeclarationSyntax EnumNode(this SyntaxNode node, int index = 0)
        {
            return node.FindNode<EnumDeclarationSyntax>(index);
        }


        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static EnumDeclarationSyntax EnumNode(this SyntaxNode node, string name)
        {
            var nodes = node.FindNodes<EnumDeclarationSyntax>();
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
