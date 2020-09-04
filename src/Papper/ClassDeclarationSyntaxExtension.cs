using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.CodeAnalysis
{
    public static class ClassDeclarationSyntaxExtension
    {

        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static ClassDeclarationSyntax ClassNode(this SyntaxNode node, int index = 0)
        {
            return node.FindNode<ClassDeclarationSyntax>(index);
        }


        /// <summary>
        /// 获取类型定义节点
        /// </summary>
        /// <returns></returns>
        public static ClassDeclarationSyntax ClassNode(this SyntaxNode node, string name)
        {
            var nodes = node.FindNodes<ClassDeclarationSyntax>();
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
