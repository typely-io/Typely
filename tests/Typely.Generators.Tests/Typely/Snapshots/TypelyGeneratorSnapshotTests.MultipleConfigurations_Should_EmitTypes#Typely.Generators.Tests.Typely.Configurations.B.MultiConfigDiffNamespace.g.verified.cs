﻿//HintName: Typely.Generators.Tests.Typely.Configurations.B.MultiConfigDiffNamespace.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;

#nullable enable

namespace Typely.Generators.Tests.Typely.Configurations.B
{
    [TypeConverter(typeof(TypelyTypeConverter<int, MultiConfigDiffNamespace>))]
    [JsonConverter(typeof(TypelyJsonConverter<int, MultiConfigDiffNamespace>))]
    public partial struct MultiConfigDiffNamespace : ITypelyValue<int, MultiConfigDiffNamespace>, IEquatable<MultiConfigDiffNamespace>, IComparable<MultiConfigDiffNamespace>, IComparable
    {
        public int Value { get; private set; }                    

        public MultiConfigDiffNamespace(int value)
        {
            TypelyValue.ValidateAndThrow<int, MultiConfigDiffNamespace>(value);
            Value = value;
        }

        public static ValidationError? Validate(int value) => null;

        public static MultiConfigDiffNamespace From(int value) => new(value);

        public static bool TryFrom(int value, out MultiConfigDiffNamespace typelyType, out ValidationError? validationError)
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
        
        public static bool TryParse(string? value, IFormatProvider? provider, out MultiConfigDiffNamespace valueObject)
        {
            if(int.TryParse(value, out var underlyingValue))
            {
                valueObject = From(underlyingValue);
                return true;
            }
                
            valueObject = default;
            return false;
        }

        public override string ToString() => Value.ToString();

        public static bool operator !=(MultiConfigDiffNamespace left, MultiConfigDiffNamespace right) => !(left == right);

        public static bool operator ==(MultiConfigDiffNamespace left, MultiConfigDiffNamespace right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(MultiConfigDiffNamespace other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is MultiConfigDiffNamespace && Equals((MultiConfigDiffNamespace)obj);

        public int CompareTo(MultiConfigDiffNamespace other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not MultiConfigDiffNamespace ? 1 : CompareTo((MultiConfigDiffNamespace)obj!);

        public static explicit operator int(MultiConfigDiffNamespace value) => value.Value;
    }
}