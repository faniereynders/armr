using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace ArmrAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class ArmrAnalyzerAnalyzer : DiagnosticAnalyzer
    {
        public const string DiagnosticId = "ParametersAnalyzer";

        private static readonly string Title = "Parameter should be defined";
        private static readonly string MessageFormat = "Parameter {0} is used but not defined";
        private static readonly string Description = "Parameters that are used should be defined for a template to be valid";
        private const string Category = "Syntax";

        private static DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get { return ImmutableArray.Create(Rule); } }

        public override void Initialize(AnalysisContext context)
        {
            context.RegisterSyntaxNodeAction(AnalyzeSyntaxNode, SyntaxKind.ClassDeclaration);
        }

        private static void AnalyzeSyntaxNode(SyntaxNodeAnalysisContext context)
        {
            var node = (ClassDeclarationSyntax)context.Node;

            var baseToken = node.BaseList.ChildNodes().OfType<SimpleBaseTypeSyntax>().FirstOrDefault().GetFirstToken();
            if (baseToken.Text == "ArmTemplate")
            {
                var parametersMethod = node.ChildNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault(m => ((MethodDeclarationSyntax)m).Identifier.Text == "Parameters");
            }
        }
    }
}
