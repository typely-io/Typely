﻿namespace Typely.Core.Extensions;

/// <summary>
/// Type extensions
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Determines whether <see cref="ITypelyValue{TValue,TTypelyValue}"/> is implemented by the current <see cref="Type"/>.
    /// </summary>
    /// <param name="type">The type to be verified.</param>
    /// <returns><see langword="true" /> when the type implements the interface.</returns>
    public static bool ImplementsITypelyValue(this Type type)
    {
        Type underlyingType;

        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            underlyingType = Nullable.GetUnderlyingType(type)!;
        }
        else
        {
            underlyingType = type;
        }

        var typelyType = typeof(ITypelyValue<,>);
        return underlyingType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typelyType);
    }
}