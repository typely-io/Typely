﻿//HintName: Typely.Generators.Tests.Typely.Specifications.ShouldGenerate.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;

#nullable enable

namespace Typely.Generators.Tests.Typely.Specifications
{
    [TypeConverter(typeof(TypelyTypeConverter<string, ShouldGenerate>))]
    [JsonConverter(typeof(TypelyJsonConverter<string, ShouldGenerate>))]
    public partial class ShouldGenerate : ITypelyValue<string, ShouldGenerate>, IEquatable<ShouldGenerate>, IComparable<ShouldGenerate>, IComparable
    {
        public string Value { get;  }

        private ShouldGenerate(string value, bool bypassValidation)
        {
            Value = value;
        }

        public ShouldGenerate(string value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(ShouldGenerate));
            TypelyValue.ValidateAndThrow<string, ShouldGenerate>(value);
            Value = value;
        }

        public static ValidationError? Validate(string value)
        {
            return null;
        }

        public static ShouldGenerate From(string value) => new(value);

        public static bool TryFrom(string value, out ShouldGenerate? typelyType, out ValidationError? validationError)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(ShouldGenerate));
            validationError = Validate(value);
            var isValid = validationError == null;

            typelyType = isValid ? new ShouldGenerate(value, true) : null;

            return isValid;
        }
        
        public static bool TryParse(string? value, out ShouldGenerate? valueObject) =>
            TryParse(value, null, out valueObject);

        public static bool TryParse(string? value, IFormatProvider? provider, out ShouldGenerate? valueObject)
        {
           if(value is null)
           {
               valueObject = null;
               return false;
           }
        
            valueObject = From(value!);
            return true;
        }

        public override string ToString() => Value.ToString();

        public static bool operator !=(ShouldGenerate? left, ShouldGenerate? right) => !(left == right);

        public static bool operator ==(ShouldGenerate? left, ShouldGenerate? right) => left?.Equals(right) ?? false;

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(ShouldGenerate? other)
        {
            if(ReferenceEquals(this, other))
            {
                return true;
            }

            return !ReferenceEquals(other, null) && Value.Equals(other.Value);
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is ShouldGenerate type && Equals(type);

        public int CompareTo(ShouldGenerate? other) => ReferenceEquals(other, null) ? 1 : Value.CompareTo(other.Value);
                                                                            
        public int CompareTo(object? obj) => obj is not ShouldGenerate type ? 1 : CompareTo(type);

        public static explicit operator string(ShouldGenerate value) => value.Value;
    }
}