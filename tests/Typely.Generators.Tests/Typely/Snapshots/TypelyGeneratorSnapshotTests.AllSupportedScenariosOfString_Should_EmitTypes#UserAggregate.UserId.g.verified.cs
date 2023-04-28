﻿//HintName: UserAggregate.UserId.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;

#nullable enable

namespace UserAggregate
{
    [TypeConverter(typeof(TypelyTypeConverter<string, UserId>))]
    [JsonConverter(typeof(TypelyJsonConverter<string, UserId>))]
    public partial struct UserId : ITypelyValue<string, UserId>, IEquatable<UserId>, IComparable<UserId>, IComparable, IMaxLength
    {
        public static int MaxLength => 20;

        public string Value { get; private set; }                    

        public UserId(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(UserId));
            value = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
            TypelyValue.ValidateAndThrow<string, UserId>(value);
            Value = value;
        }

        public static ValidationError? Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return ValidationErrorFactory.Create(value, "NotEmpty", ErrorMessages.NotEmpty, "Owner identifier");
            }

            if (value.Equals("0"))
            {
                return ValidationErrorFactory.Create(value, "ERR001", "{Name} cannot be equal to {ComparisonValue}.", "Owner identifier",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", "0" },
                    });
            }

            if (value.Length > 20)
            {
                return ValidationErrorFactory.Create(value, "MaxLength", ErrorMessages.MaxLength, "Owner identifier",
                    new Dictionary<string, object?>
                    {
                        { "MaxLength", 20 },
                    });
            }

            if (!(value != "1" && value.ToLower() == "12"))
            {
                return ValidationErrorFactory.Create(value, "Must", ErrorMessages.Must, "Owner identifier");
            }

            return null;
        }

        public static UserId From(string value) => new(value);

        public static bool TryFrom(string value, [MaybeNullWhen(false)] out UserId typelyType, out ValidationError? validationError)
        {
            if (value == null) throw new ArgumentNullException(nameof(UserId));
            value = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(value);
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

        public static bool operator !=(UserId left, UserId right) => !(left == right);

        public static bool operator ==(UserId left, UserId right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(UserId other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is UserId && Equals((UserId)obj);

        public int CompareTo(UserId other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not UserId ? 1 : CompareTo((UserId)obj!);

        public static explicit operator string(UserId value) => value.Value;
    }
}