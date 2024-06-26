﻿using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.GetById;
public class Validator : AbstractValidator<QueryModel>
{
    public Validator()
    {
        RuleFor(query => query).NotEmpty().NotNull();
        RuleFor(x => x.Id).NotEmpty().NotNull().GreaterThan(0);
    }
}
