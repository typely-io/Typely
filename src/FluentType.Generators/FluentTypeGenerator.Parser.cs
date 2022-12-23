﻿using FluentType.Core;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis;
using FluentType.SourceGenerators.Extensions;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using FluentType.Generators.Extensions;
using Basic.Reference.Assemblies;

namespace FluentType.Generators;

public partial class FluentTypeGenerator
{
    internal sealed class Parser : IDisposable
    {
        private readonly CancellationToken _cancellationToken;
        private readonly Compilation _compilation;
        private readonly Action<Diagnostic> _reportDiagnostic;

        public Parser(Compilation compilation, Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
        {
            _compilation = compilation;
            _cancellationToken = cancellationToken;
            _reportDiagnostic = reportDiagnostic;

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        /// <summary>
        /// Filter classes having an interface name <see cref="IFluentTypesConfiguration"/>.
        /// </summary>
        internal static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode) =>
            syntaxNode is ClassDeclarationSyntax c && c.HasInterface(nameof(IFluentTypesConfiguration));

        /// <summary>
        /// Filter classes having an interface <see cref="IFluentTypesConfiguration"/> that matches the 
        /// namespace and returns the <see cref="ClassDeclarationSyntax"/>.
        /// </summary>
        internal static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
            var classSymbol = context.SemanticModel.GetDeclaredSymbol(classDeclarationSyntax);
            if (classSymbol == null)
            {
                return null;
            }
            return classSymbol.AllInterfaces.Any(x => x.ToString() == typeof(IFluentTypesConfiguration).FullName) ?
                classDeclarationSyntax : null;
        }

        /// <summary>
        /// Execute the different <see cref="IFluentTypesConfiguration"/> classes founds and generate models of the desired user types.
        /// </summary>
        /// <param name="classes">Classes to parse.</param>
        /// <returns>A list of representation of desired user types.</returns>
        public IReadOnlyList<FluentTypeModel> GetFluentTypes(IEnumerable<ClassDeclarationSyntax> classes)
        {
            // We enumerate by syntax tree, to minimize impact on performance
            return classes.GroupBy(x => x.SyntaxTree).SelectMany(x => GetFluentTypes(x.Key)).ToList().AsReadOnly();
        }

        /// <summary>
        /// Execute the different <see cref="IFluentTypesConfiguration"/> classes and generate models of the desired user types.
        /// </summary>
        /// <param name="syntaxTree">SyntaxTree to parse</param>
        /// <returns>A list of representation of desired user types.</returns>
        private IEnumerable<FluentTypeModel> GetFluentTypes(SyntaxTree syntaxTree)
        {
            // Stop if we're asked to
            _cancellationToken.ThrowIfCancellationRequested();

            var configurationAssembly = CompileUserCodeTypesConfiguration(syntaxTree);
            if (configurationAssembly == null)
            {
                return Array.Empty<FluentTypeModel>();
            }

            var configurationTypes = configurationAssembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IFluentTypesConfiguration)))
                .ToList();

            foreach (var configurationType in configurationTypes)
            {
                var fluentTypeConfiguration = (IFluentTypesConfiguration)configurationAssembly.CreateInstance(configurationType.FullName);
                var fluentBuilder = new FluentTypeBuilder();
                fluentTypeConfiguration.Configure(fluentBuilder);
                var called = fluentBuilder.Called;
            }

            return Array.Empty<FluentTypeModel>();
        }

        /// <summary>
        /// Only resolve known assemblies. 
        /// </summary>
        private Assembly? CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) =>
            args.Name == "FluentType.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
                ? AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName == args.Name)
                : null;

        private Assembly? CompileUserCodeTypesConfiguration(SyntaxTree syntaxTree)
        {
            var compilation = CSharpCompilation.Create(assemblyName: $"FluentType_{Path.GetRandomFileName()}")
                .WithReferenceAssemblies(ReferenceAssemblyKind.NetStandard20)
                .AddReferences(typeof(IFluentTypesConfiguration))
                .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                .AddSyntaxTrees(syntaxTree);
            

            using var ms = new MemoryStream();
            var result = compilation.Emit(ms);

            if (!result.Success)
            {
                foreach (var diagnostic in result.Diagnostics)
                {
                    _reportDiagnostic(diagnostic);
                }
                return null;
            }

            ms.Seek(0, SeekOrigin.Begin);
            return Assembly.Load(ms.ToArray());
        }

        private void Diag(DiagnosticDescriptor desc, Location? location, params object?[]? messageArgs)
        {
            _reportDiagnostic(Diagnostic.Create(desc, location, messageArgs));
        }

        public void Dispose()
        {
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            GC.SuppressFinalize(this);
        }
    }

    //Diag(new DiagnosticDescriptor(
    //                id: "AD",
    //                title: "my title",
    //                messageFormat: "my message",
    //                category: "category",
    //                DiagnosticSeverity.Error,
    //                isEnabledByDefault: true), null, null);

    /// <summary>
    /// Define the representation of a desired user type.
    /// </summary>
    internal class FluentTypeModel
    {
    }

    public class FluentTypeBuilder : IFluentTypeBuilder
    {
        public int Called { get; set; }

        public IFluentTypeBuilder<T> For<T>(string typeName)
        {
            Called++;
            return default;
        }
    }
}
