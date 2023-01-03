﻿using Microsoft.CodeAnalysis;
using Typely.Core.Builders;

namespace Typely.Generators.Typely.Parsing;

internal class TypelyBuilder : ITypelyBuilder
{
    private List<EmittableType> _emittableTypes = new List<EmittableType>();
    private SyntaxTree _syntaxTree;
    private readonly Type _configurationType;

    public TypelyBuilder(SyntaxTree syntaxTree, Type configurationType)
    {
        _syntaxTree = syntaxTree;
        _configurationType = configurationType;
    }

    public ITypelyBuilder<T> Of<T>()
    {
        var emittableType = new EmittableType(
            syntaxTree: _syntaxTree,
            underlyingType: typeof(T),
            @namespace: _configurationType.Namespace);

        _emittableTypes.Add(emittableType);

        return new RuleBuilder<T>(emittableType, _emittableTypes);
    }

    public IReadOnlyList<EmittableType> GetEmittableTypes() => _emittableTypes.AsReadOnly();
}

