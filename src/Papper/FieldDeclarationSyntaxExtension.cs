using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.CodeAnalysis
{
    public static class FieldDeclarationSyntaxExtension
    {
        /// <summary>
        /// 获取所有字段定义节点
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<FieldDeclarationSyntax> FieldNodes(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax.FindNodes<FieldDeclarationSyntax>();
        }



        public static FieldDeclarationSyntax FieldNode(this TypeDeclarationSyntax typeDeclarationSyntax, string fieldName)
        {

            var result = FieldNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                foreach (var item in result)
                {
                    if (item.Declaration.Variables.ToString() == fieldName)
                    {
                        return item;

                    }
                }
            }
            return default;
        }


        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> Names(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var result = FieldNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                return result.Select(item => item.Declaration.Variables[0].Identifier.Text);
            }
            return default;
        }

        public static IEnumerable<string> FullScripts(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var result = FieldNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                return result.Select(item => item.ToFullString());
            }
            return default;
        }


        public static string DeclarationText(this FieldDeclarationSyntax fieldDeclarationSyntax)
        {

            if (fieldDeclarationSyntax != default)
            {
                return fieldDeclarationSyntax.Modifiers.ToFullString() + fieldDeclarationSyntax.Declaration.ToFullString();
            }
            return default;

        }
    }
}
