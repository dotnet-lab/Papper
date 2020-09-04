using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.CodeAnalysis
{

    public static class AttributeDeclarationSyntaxExtension
    {

        public static IEnumerable<AttributeListSyntax> AttributeNodes(this MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax.AttributeLists;
        }
        public static IEnumerable<string> AttributeNames(this MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax.AttributeLists.SelectMany(item=>item.Attributes.Select(attr=>attr.Name.ToString()));
        }
        public static string AttributeFullScript(this MemberDeclarationSyntax memberDeclarationSyntax)
        {
            return memberDeclarationSyntax.AttributeLists.ToFullString();
        }

    }
}
