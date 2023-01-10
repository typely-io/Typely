﻿namespace Typely.Core;

/// <summary>
/// Static methods for a type generated by Typely.
/// </summary>
public static class TypelyValue
{
    /// <summary>
    /// Validates the value object and throw if in error.
    /// </summary>
    /// <typeparam name="TValue">Underlying type of the value object.</typeparam>
    /// <typeparam name="TThis">Value object's type.</typeparam>
    /// <param name="value">Value to validate.</param>
    /// <exception cref="ValidationException"></exception>
    public static void ValidateAndThrow<TValue, TThis>(TValue value) where TThis : ITypelyValue<TValue, TThis>
    {
#if NET7_0_OR_GREATER
        var validationError = TThis.Validate(value);
        if (validationError != null)
        {
            throw new ValidationException(validationError.ToString());
        }
#else
        throw new NotImplementedException();
#endif
    }
}