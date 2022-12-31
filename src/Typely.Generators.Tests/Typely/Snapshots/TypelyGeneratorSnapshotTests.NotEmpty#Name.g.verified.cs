﻿//HintName: Name.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using Typely.Core;
using Typely.Core.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

#nullable enable

namespace Typely.Generators.Tests.Typely.Configurations
{
    [JsonConverter(typeof(TypelyJsonConverter<String, Name>))]
    public partial struct Name : ITypelyValue<String, Name>
    {
        public String Value { get; private set; }

        public Name() => throw new Exception("Parameterless constructor is not allowed.");

        public Name(String value)
        {
            TypelyValue.ValidateAndThrow<String, Name>(value);
            Value = value;
        }

        public static ValidationError? Validate(String value) 
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, "Name");
            }

            return null;
        }

        public static Name From(String value) => new(value);

        public static bool TryFrom(String value, [MaybeNullWhen(false)] out Name typelyType, out ValidationError? validationError)
        {
            validationError = Validate(value);
            var isValid = validationError == null;
            typelyType = default;
            if (isValid)
            {
                typelyType.Value = value;
            }
            return isValid;
        }
    }
}