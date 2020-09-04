using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.CodeAnalysis
{
    public static class PropertyDeclarationSyntaxExtension
    {
        /// <summary>
        /// 获取所有字段定义节点
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<PropertyDeclarationSyntax> PropertyNodes(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax.FindNodes<PropertyDeclarationSyntax>();
        }

        public static PropertyDeclarationSyntax PropertyNode(this TypeDeclarationSyntax typeDeclarationSyntax, string propertyName)
        {

            var result = PropertyNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                foreach (var item in result)
                {
                    if (item.Identifier.Text == propertyName)
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
            var result = PropertyNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                return result.Select(item => item.Identifier.Text);
            }
            return default;
        }

        public static IEnumerable<string> FullScripts(this TypeDeclarationSyntax typeDeclarationSyntax)
        {

            var result = PropertyNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                return result.Select(item => item.ToFullString());
            }
            return default;

        }
        
        public static string GetGetText(this PropertyDeclarationSyntax propertyDeclarationSyntax)
        {

            if (propertyDeclarationSyntax != default)
            {
                foreach (var accessor in propertyDeclarationSyntax.AccessorList.Accessors)
                {
                    if (accessor.Keyword.Text == "get")
                    {
                        return accessor.Body.Statements.ToFullString();
                    }
                }

            }
            return default;
        }
        public static string GetSetText(this PropertyDeclarationSyntax propertyDeclarationSyntax)
        {

            if (propertyDeclarationSyntax != default)
            {
                foreach (var accessor in propertyDeclarationSyntax.AccessorList.Accessors)
                {
                    if (accessor.Keyword.Text == "set")
                    {
                        return accessor.Body.Statements.ToFullString();
                    }
                }

            }
            return default;
        }

        public static string DeclarationText(this PropertyDeclarationSyntax propertyDeclarationSyntax)
        {

            if (propertyDeclarationSyntax != default)
            {
                return propertyDeclarationSyntax.Modifiers.ToFullString() + propertyDeclarationSyntax.Type.ToFullString() + propertyDeclarationSyntax.Identifier.ToFullString();
            }
            return default;

        }
    }
}
