﻿using dotnet_core_blogs_architecture.Data.Results;
using MediatR;

namespace dotnet_core_blogs_architecture.blogs.Mediator.Comment.Queries.GetById;
public class QueryModel : IRequest<ValidationResult>
{
    public long Id { get; set; }
}
