using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.CodeAnalysis
{
    public static class MethodDeclarationSyntaxExtension
    {
        /// <summary>
        /// 获取所有字段定义节点
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<MethodDeclarationSyntax> MethodNodes(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            return typeDeclarationSyntax.FindNodes<MethodDeclarationSyntax>();
        }
        

        /// <summary>
        /// 获取字段名
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> MethodNames(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var result = MethodNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                return result.Select(item => item.Identifier.Text);
            }
            return default;
        }

        public static IEnumerable<string> MethodFullScripts(this TypeDeclarationSyntax typeDeclarationSyntax)
        {
            var result = MethodNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                return result.Select(item => item.ToFullString());
            }
            return default;
        }

        public static MethodDeclarationSyntax MethodNode(this TypeDeclarationSyntax typeDeclarationSyntax, string methodName)
        {
            var result = MethodNodes(typeDeclarationSyntax);
            if (result.Count() != 0)
            {
                foreach (var item in result)
                {
                    if (item.Identifier.Text == methodName)
                    {
                        return item;
                    }
                }
            }
            return default;
        }


        public static string Body(this MethodDeclarationSyntax methodDeclarationSyntax)
        {
            return methodDeclarationSyntax == default ? default : methodDeclarationSyntax.Body.Statements.ToFullString();
        }


        public static string ReturnType(this MethodDeclarationSyntax methodDeclarationSyntax)
        {
            return methodDeclarationSyntax.ReturnType.FullScript();
        }


        public static IEnumerable<string> ParametersType(this MethodDeclarationSyntax methodDeclarationSyntax, bool withModifiers = true)
        {

            if (withModifiers)
            {
                return methodDeclarationSyntax == default ? default : methodDeclarationSyntax.ParameterList.Parameters.Select(item => (item.Modifiers.ToFullString() + item.Type.ToFullString()));
            }
            else
            {
                return methodDeclarationSyntax == default ? default : methodDeclarationSyntax.ParameterList.Parameters.Select(item => item.Type.ToFullString());
            }

        }

        public static IEnumerable<string> Parameters(this MethodDeclarationSyntax methodDeclarationSyntax)
        {

            return methodDeclarationSyntax == default ? default : methodDeclarationSyntax.ParameterList.Parameters.Select(item => item.ToFullString());

        }
        public static IEnumerable<string> ParametersName(this MethodDeclarationSyntax methodDeclarationSyntax)
        {

            return methodDeclarationSyntax == default ? default : methodDeclarationSyntax.ParameterList.Parameters.Select(item => item.Identifier.Text);

        }
        public static string ReturnExpression(this MethodDeclarationSyntax methodDeclarationSyntax)
        {

            var returnNode = methodDeclarationSyntax.Body.Statements.OfType<ReturnStatementSyntax>();
            return returnNode.Count() != 0 ? returnNode.First().ToFullString() : default;

        }

        public static string DeclarationText(this MethodDeclarationSyntax methodDeclarationSyntax)
        {

            if (methodDeclarationSyntax != default)
            {
                return methodDeclarationSyntax.Modifiers.ToFullString() 
                    + methodDeclarationSyntax.ReturnType.ToFullString() 
                    + methodDeclarationSyntax.Identifier.ToFullString()
                    + methodDeclarationSyntax.ParameterList.ToFullString();
            }
            return default;

        }
    }
}
