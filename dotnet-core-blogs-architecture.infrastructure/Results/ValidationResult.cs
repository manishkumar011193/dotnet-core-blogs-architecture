using dotnet_core_blogs_architecture.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_core_blogs_architecture.Data.Results;

public abstract class ValidationResult
{
    public IList<ErrorMessage> Errors { get; private set; }

    public ValidationResult()
    {
        Errors = new List<ErrorMessage>();
    }

    public void AddError(string field, string description)
    {
        Errors.Add(new ErrorMessage()
        {
            Field = field,
            Description = new List<string>() { description }
        });
    }
}
