﻿//HintName: Election.Votes.g.cs
// <auto-generated>This file was generated by Typely.</auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using Typely.Core;
using Typely.Core.Converters;
using Typely.Generators.Tests.Typely.Configurations;

#nullable enable

namespace Election
{
    [TypeConverter(typeof(TypelyTypeConverter<DateTimeOffset, Votes>))]
    [JsonConverter(typeof(TypelyJsonConverter<DateTimeOffset, Votes>))]
    public partial struct Votes : ITypelyValue<DateTimeOffset, Votes>, IEquatable<Votes>, IComparable<Votes>, IComparable
    {
        public DateTimeOffset Value { get; private set; }

        public Votes(DateTimeOffset value)
        {
            TypelyValue.ValidateAndThrow<DateTimeOffset, Votes>(value);
            Value = value;
        }

        public static ValidationError? Validate(DateTimeOffset value)
        {
            if (value == default)
            {
                return ValidationErrorFactory.Create(value, "ERR-001", "The value cannot be empty", "Presidency vote");
            }

            if (value.Equals(new DateTimeOffset(new DateTime(2022,1,1))))
            {
                return ValidationErrorFactory.Create(value, "NotEqual", ErrorMessages.NotEqual, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new DateTimeOffset(new DateTime(2022,1,1)) },
                    });
            }

            if (!(value == new DateTimeOffset(new DateTime(2022,1,1))))
            {
                return ValidationErrorFactory.Create(value, "Must", ErrorMessages.Must, "Presidency vote");
            }

            if (!(!value.Equals(10)))
            {
                return ValidationErrorFactory.Create(value, "Must", ErrorMessages.Must, "Presidency vote");
            }

            if (value <= new DateTimeOffset(new DateTime(2022,1,1)))
            {
                return ValidationErrorFactory.Create(value, "GreaterThan", LocalizedMessages.CustomMessage, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new DateTimeOffset(new DateTime(2022,1,1)) },
                    });
            }

            if (value < new DateTimeOffset(new DateTime(2022,1,1)))
            {
                return ValidationErrorFactory.Create(value, "GreaterThanOrEqualTo", A.CustomLocalization.Value, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new DateTimeOffset(new DateTime(2022,1,1)) },
                    });
            }

            if (value >= new DateTimeOffset(new DateTime(2022,1,1)))
            {
                return ValidationErrorFactory.Create(value, "LessThan", ErrorMessages.LessThan, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new DateTimeOffset(new DateTime(2022,1,1)) },
                    });
            }

            if (value > new DateTimeOffset(new DateTime(2022,1,1)))
            {
                return ValidationErrorFactory.Create(value, "LessThanOrEqualTo", ErrorMessages.LessThanOrEqualTo, "Presidency vote",
                    new Dictionary<string, object?>
                    {
                        { "ComparisonValue", new DateTimeOffset(new DateTime(2022,1,1)) },
                    });
            }

            return null;
        }

        public static Votes From(DateTimeOffset value) => new(value);

        public static bool TryFrom(DateTimeOffset value, out Votes typelyType, out ValidationError? validationError)
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
        
        public static bool TryParse(string? value, IFormatProvider? provider, out Votes valueObject)
        {
            if(DateTimeOffset.TryParse(value, out var underlyingValue))
            {
                valueObject = From(underlyingValue);
                return true;
            }
                
            valueObject = default;
            return false;
        }

        public override string ToString() => Value.ToString();

        public static bool operator !=(Votes left, Votes right) => !(left == right);

        public static bool operator ==(Votes left, Votes right) => left.Equals(right);

        public override int GetHashCode() => Value.GetHashCode();

        public bool Equals(Votes other) => Value.Equals(other.Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is Votes && Equals((Votes)obj);

        public int CompareTo(Votes other) => Value.CompareTo(other.Value);

        public int CompareTo(object? obj) => obj is not Votes ? 1 : CompareTo((Votes)obj!);

        public static explicit operator DateTimeOffset(Votes value) => value.Value;
    }
}