﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Typely.Generators.Extensions;
using Typely.Generators.Typely.Parsing.TypeBuilders;

namespace Typely.Generators.Typely.Parsing;

/// <summary>
/// Parse a <see cref="ClassDeclarationSyntax"/> and generates a list of <see cref="EmittableTypeBuilder"/>.
/// </summary>
internal static class Parser
{
    /// <summary>
    /// Filter classes having an interface name "ITypelySpecification".
    /// </summary>
    internal static bool IsTypelySpecificationClass(SyntaxNode syntaxNode, CancellationToken cancellationToken) =>
        syntaxNode is ClassDeclarationSyntax c && IsTypelySpecificationClass(c);

    /// <summary>
    /// Filter classes having an interface name "ITypelySpecification".
    /// </summary>
    private static bool IsTypelySpecificationClass(ClassDeclarationSyntax syntax) =>
        syntax.HasInterface(TypelySpecification.InterfaceName);

    /// <summary>
    /// Filter classes having an interface full name "Typely.Core.ITypelySpecification".
    /// </summary>
    private static bool IsTypelySpecificationClass(SemanticModel model, ClassDeclarationSyntax classDeclarationSyntax)
    {
        var classSymbol = model.GetDeclaredSymbol(classDeclarationSyntax)!;
        return classSymbol.AllInterfaces.Any(x => x.ToString() == TypelySpecification.FullInterfaceName);
    }

    /// <summary>
    /// Filter classes having an interface "ITypelySpecification" and generate models of the desired user types.
    /// </summary>
    /// <param name="context">The generator's context.</param>
    /// <param name="cancellationToken">A token to notify the operation should be cancelled.</param>
    /// <returns>A list of representation of desired user types.</returns>
    internal static IReadOnlyList<EmittableType>? GetEmittableTypesForClass(GeneratorSyntaxContext context,
        CancellationToken cancellationToken)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        return IsTypelySpecificationClass(context.SemanticModel, classDeclarationSyntax)
            ? GetEmittableTypesForClass(classDeclarationSyntax, context.SemanticModel, cancellationToken)
            : null;
    }

    /// <summary>
    /// Filter classes having an interface "ITypelySpecification" and generate models of the desired user types.
    /// </summary>
    /// <param name="context">The generator's context.</param>
    /// <param name="semanticModel"></param>
    /// <param name="cancellationToken">A token to notify the operation should be cancelled.</param>
    /// <param name="classDeclarationSyntax">The class containing the specification.</param>
    /// <returns>A list of representation of desired user types.</returns>
    internal static IReadOnlyList<EmittableType> GetEmittableTypesForClass(
        ClassDeclarationSyntax classDeclarationSyntax, SemanticModel semanticModel, CancellationToken cancellationToken)
    {
        var emittableTypes = new List<EmittableType>();
        var classSyntaxes = classDeclarationSyntax.SyntaxTree
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .Where(IsTypelySpecificationClass)
            .ToList();

        foreach (var classSyntax in classSyntaxes)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var classEmittableTypes = ParseClass(classSyntax, semanticModel);
            emittableTypes.AddRange(classEmittableTypes);
        }

        return emittableTypes;
    }

    /// <summary>
    /// Filter methods having a name that matches <see cref="TypelySpecification.MethodName"/>.
    /// </summary>
    private static bool IsCreateMethod(SyntaxNode syntaxNode) =>
        syntaxNode is MethodDeclarationSyntax { Identifier.Text: TypelySpecification.MethodName };

    /// <summary>
    /// Parse a <see cref="ClassDeclarationSyntax"/> and generate a list of <see cref="EmittableType"/>.
    /// </summary>
    private static IEnumerable<EmittableType> ParseClass(ClassDeclarationSyntax classSyntax, SemanticModel model)
    {
        var methodSyntax = classSyntax.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .First(IsCreateMethod);

        var emittableTypes = new List<EmittableType>();
        var typelyBuilderParameterName = methodSyntax.ParameterList.Parameters.First().Identifier.Text;
        var parsedStatements = ParseStatements(methodSyntax, typelyBuilderParameterName, model);
        var defaultNamespace = GetNamespace(classSyntax);

        foreach (var parsedStatement in parsedStatements)
        {
            var emittableType = EmittableTypeBuilderFactory.Create(defaultNamespace, parsedStatement).Build();
            if (emittableType != null)
            {
                emittableTypes.Add(emittableType);
            }
        }

        return emittableTypes;
    }

    /// <summary>
    /// Parse each line of code of a method.
    /// </summary>
    /// <param name="methodDeclarationSyntax">The <see cref="MethodDeclarationSyntax"/>.</param>
    /// <param name="typelyBuilderParameterName">The builder parameter name used in "ITypelySpecification.Configure".</param>
    /// <param name="model">The <see cref="SemanticModel"/>.</param>
    /// <returns>Return a list of <see cref="ParseDeclarationStatement"/>.</returns>
    private static List<ParsedStatement> ParseStatements(MethodDeclarationSyntax methodDeclarationSyntax,
        string typelyBuilderParameterName, SemanticModel model)
    {
        var bodySyntaxNodes = methodDeclarationSyntax.Body!.DescendantNodes()
            .Where(x => x is ExpressionStatementSyntax or LocalDeclarationStatementSyntax);
        var parsedStatements = new List<ParsedStatement>();
        var parsedStatementVariables = new Dictionary<string, ParsedStatement>();

        foreach (var bodySyntaxNode in bodySyntaxNodes)
        {
            var parsedStatement = new ParsedStatement(model);

            if (bodySyntaxNode is ExpressionStatementSyntax expressionStatementSyntax)
            {
                parsedStatements.Add(parsedStatement);
                ParseInvocationExpression(expressionStatementSyntax.Expression, parsedStatement);
            }
            else if (bodySyntaxNode is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
            {
                ParseDeclarationStatement(parsedStatementVariables, parsedStatement, localDeclarationStatementSyntax);
            }

            if (IsStatementInvalid(parsedStatement))
            {
                parsedStatements.Remove(parsedStatement);
                continue;
            }

            MergeStatementVariable(parsedStatement);
        }

        return parsedStatements;


        void MergeStatementVariable(ParsedStatement parsedStatement)
        {
            if (!UseStatementVariable(parsedStatement))
            {
                return;
            }

            var parsedStatementVariable = parsedStatementVariables[parsedStatement.Root];
            parsedStatement.Invocations.InsertRange(0, parsedStatementVariable.Invocations);
            parsedStatement.Root = parsedStatementVariable.Root;
        }

        bool IsStatementInvalid(ParsedStatement parsedStatement)
        {
            if (!parsedStatement.IsValid())
            {
                return true;
            }

            if (UseStatementVariable(parsedStatement))
            {
                return !parsedStatementVariables.ContainsKey(parsedStatement.Root) ||
                 !parsedStatementVariables[parsedStatement.Root].IsValid();
            }

            return false;
        }

        bool UseStatementVariable(ParsedStatement invocationResult) =>
            invocationResult.Root != typelyBuilderParameterName;
    }

    /// <summary>
    /// Parse a <see cref="LocalDeclarationStatementSyntax"/>.
    /// ex: var vote = builder.OfInt().For("Vote");
    /// </summary>
    private static void ParseDeclarationStatement(
        Dictionary<string, ParsedStatement> parsedExpressionVariables,
        ParsedStatement parsed, LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
    {
        var variable = localDeclarationStatementSyntax.Declaration.Variables.First();
        parsedExpressionVariables.Add(variable.Identifier.Text, parsed);
        ParseInvocationExpression(variable.Initializer!.Value, parsed);
    }

    /// <summary>
    /// Get the namespace of a <see cref="SyntaxNode"/>.
    /// </summary>
    private static string GetNamespace(SyntaxNode classSyntax)
    {
        while (classSyntax.Parent != null)
        {
            switch (classSyntax.Parent)
            {
                case NamespaceDeclarationSyntax namespaceDeclarationSyntax:
                    return namespaceDeclarationSyntax.Name.ToString();
                case FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax:
                    return fileScopedNamespaceDeclarationSyntax.Name.ToString();
                default:
                    classSyntax = classSyntax.Parent;
                    break;
            }
        }

        return string.Empty;
    }

    /// <summary>
    /// Parse a <see cref="SyntaxNode"/> as an <see cref="InvocationExpressionSyntax"/> to get the member name and the argument list.
    /// </summary>
    private static void ParseInvocationExpression(ExpressionSyntax syntaxNode,
        ParsedStatement parsed)
    {
        // ex: builder.OfInt().For("Vote").WithNamespace("UserAggregate").WithName("Vote")
        if (syntaxNode is InvocationExpressionSyntax invocationExpressionSyntax)
        {
            if (invocationExpressionSyntax.Expression is MemberAccessExpressionSyntax memberAccessExpressionSyntax)
            {
                var memberName = memberAccessExpressionSyntax.Name.Identifier.Text;
                var argumentList = invocationExpressionSyntax.ArgumentList;

                parsed.Invocations.Insert(0, new ParsedInvocation(argumentList, memberName));

                // ex: builder.OfInt().For("Vote").WithNamespace("UserAggregate").WithName()
                ParseInvocationExpression(memberAccessExpressionSyntax.Expression, parsed);
            }
        }
        // ex: builder
        else if (syntaxNode is IdentifierNameSyntax nameSyntax)
        {
            parsed.Root = nameSyntax.Identifier.Text;
        }
    }
}