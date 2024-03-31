using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_core_blogs_architecture.Data.Results;

public class NotFoundValidationResult : ValidationResult
{
    public NotFoundValidationResult(string field, string description)
    {
        this.AddError(field, description);
    }
}
