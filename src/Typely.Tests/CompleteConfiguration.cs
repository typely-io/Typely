﻿using Typely.Core;

namespace Typely.Tests;

public class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder.For<string>("FirstName");

        builder
            .For<int>("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'Name' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual(1);

        builder.For<int>("EqualityTest");

        builder.For<int>("ValueType");
        builder.For<string>("ReferenceType");
    }
}
