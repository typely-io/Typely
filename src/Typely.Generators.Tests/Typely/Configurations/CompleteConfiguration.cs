﻿using Typely.Core;

namespace Typely.Generators.Tests.Typely.Configurations;

internal class CompleteConfiguration : ITypelyConfiguration
{
    public void Configure(ITypelyBuilder builder)
    {
        builder
            .For<int>("UserId")
            .Namespace("UserAggregate")
            .Name("Owner identifier")
            .AsStruct()
            .NotEmpty().WithMessage("'{Name}' cannot be empty.").WithErrorCode("ERR001")
            .NotEqual(1);

        builder.For<string>("Planet")
            .NotEqual("sun").WithMessage(() => ErrorMessages.NotEqual);
    }
}
