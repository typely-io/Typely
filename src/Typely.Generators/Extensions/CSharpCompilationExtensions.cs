﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;

namespace Typely.Generators.Extensions;

/// <summary>
/// Extensions over <see cref="CSharpCompilation"/>.
/// </summary>
internal static class CSharpCompilationExtensions
{
    public static CSharpCompilation AddReferences(this CSharpCompilation compilation, params Type[] references) =>
        compilation.AddReferences(references.Select(x => MetadataReference.CreateFromFile(x.Assembly.Location)));
}
