﻿//HintName: Typely.Generators.Tests.Typely.Configurations.Code.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Typely.Core;
using Typely.Core.Converters;

#nullable enable

namespace Typely.Generators.Tests.Typely.Configurations
{
    [TypeConverter(typeof(TypelyTypeConverter<string, Code>))]
    [JsonConverter(typeof(TypelyJsonConverter<string, Code>))]
    public partial class Code : ITypelyValue<string, Code>, IEquatable<Code>, IComparable<Code>, IComparable, IMaxLength
    {
        public static int MaxLength => 4;

        public string Value { get; private set; }                    

        public Code(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(Code));
            value = value.Trim().ToLower();
            TypelyValue.ValidateAndThrow<string, Code>(value);
            Value = value;
        }

        public static ValidationError? Validate(string value)
        {
            if (value.Length != 4)
            {
                return ValidationErrorFactory.Create(value, "ExactLength", ErrorMessages.ExactLength, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ExactLength", 4 },
                    });
            }

            if (value.Length < 4 || value.Length > 20)
            {
                return ValidationErrorFactory.Create(value, "Length", ErrorMessages.Length, "Code",
                    new Dictionary<string, object?>
                    {
                        { "MinLength", 4 },
                        { "MaxLength", 20 },
                    });
            }

            if (value.Equals("0000"))
            {
                return ValidationErrorFactory.Create(value, "NotEqual", ErrorMessages.NotEqual, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", "0000" },
                    });
            }

            if (!new Regex(".+").IsMatch(value))
            {
                return ValidationErrorFactory.Create(value, "Matches", ErrorMessages.Matches, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new Regex(".+") },
                    });
            }

            if (string.Compare(value, "A", StringComparison.Ordinal) <= 0)
            {
                return ValidationErrorFactory.Create(value, "GreaterThan", ErrorMessages.GreaterThan, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", "A" },
                    });
            }

            if (string.Compare(value, "A", StringComparison.Ordinal) < 0)
            {
                return ValidationErrorFactory.Create(value, "GreaterThanOrEqualTo", ErrorMessages.GreaterThanOrEqualTo, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", "A" },
                    });
            }

            if (string.Compare(value, "A", StringComparison.Ordinal) >= 0)
            {
                return ValidationErrorFactory.Create(value, "LessThan", ErrorMessages.LessThan, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", "A" },
                    });
            }

            if (string.Compare(value, "A", StringComparison.Ordinal) > 0)
            {
                return ValidationErrorFactory.Create(value, "LessThanOrEqualTo", ErrorMessages.LessThanOrEqualTo, "Code",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", "A" },
                    });
            }

            return null;
        }

        public static Code From(string value) => new(value);

        public static bool TryFrom(string value, out Code typelyType, out ValidationError? validationError)
        {
            if (value == null) throw new ArgumentNullException(nameof(Code));
            value = value.Trim().ToLower();
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

        public static bool operator !=(Code left, Code right) => !(left == right);

        public static bool operator ==(Code left, Code right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Code other) => Value?.Equals(other.Value) ?? false;

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Code && Equals((Code)obj);

        public int CompareTo(Code other) => Value?.CompareTo(other.Value) ?? 1;

        public int CompareTo(object? obj) => obj is not Code ? 1 : CompareTo((Code)obj!);

        public static explicit operator string(Code value) => value.Value;
    }
}