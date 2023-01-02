﻿//HintName: Title.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using Typely.Core;
using Typely.Core.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

#nullable enable

namespace Typely.Generators.Tests.Typely.Configurations
{
    [JsonConverter(typeof(TypelyJsonConverter<String, Title>))]
    public partial struct Title : ITypelyValue<String, Title>, IEquatable<Title>, IComparable<Title>, IComparable
    {
        public String Value { get; private set; }

        public Title() => throw new Exception("Parameterless constructor is not allowed.");

        public Title(String value)
        {
            TypelyValue.ValidateAndThrow<String, Title>(value);
            Value = value;
        }

        public static ValidationError? Validate(String value)
        {
            if (value == null) throw new ArgumentNullException(nameof(Title));

            if (string.IsNullOrWhiteSpace(value))
            {
                return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, "Title");
            }

            if (value.Length > 100)
            {
                return ValidationErrorFactory.Create(value, "MaxLength", ErrorMessages.MaxLength, "Title",
                    new Dictionary<string, object?>
                    {
                        { "MaxLength", 100 },
                    });
            }

            return null;
        }

        public static Title From(String value) => new(value);

        public static bool TryFrom(String value, [MaybeNullWhen(false)] out Title typelyType, out ValidationError? validationError)
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

        public override string ToString() => Value.ToString();

        public static bool operator !=(Title left, Title right) => !(left == right);

        public static bool operator ==(Title left, Title right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Title other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Title && Equals((Title)obj);

        public int CompareTo(Title other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not Title ? 1 : CompareTo((Title)obj!);

        public static explicit operator String(Title value) => value.Value;
    }
}