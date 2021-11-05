using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.CodeAnalysis
{

    public static class AttributeDeclarationSyntaxExtension
    {
        /// <summary>
        /// 获取成员的特性标签语法节点
        /// </summary>
        /// <param name="memberDeclarationSyntax"></param>
        /// <returns></returns>
        public static IEnumerable<AttributeListSyntax> AttributeNodes(this MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax.AttributeLists;
        }
        /// <summary>
        /// 获取成员的特性标签值
        /// </summary>
        /// <param name="memberDeclarationSyntax"></param>
        /// <returns></returns>
        public static IEnumerable<string> AttributeNames(this MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax.AttributeLists.SelectMany(item=>item.Attributes.Select(attr=>attr.Name.ToString()));
        }
        /// <summary>
        /// 获取成员的特性整个字符串
        /// </summary>
        /// <param name="memberDeclarationSyntax"></param>
        /// <returns></returns>
        public static string AttributeFullScript(this MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax.AttributeLists.ToFullString();
        }

    }
}
