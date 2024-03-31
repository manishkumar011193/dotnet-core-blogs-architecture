using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_core_blogs_architecture.Data.Results;

public class ValidObjectResult : ValidationResult
{
    public dynamic Data { get; }

    public ValidObjectResult(dynamic data)
    {
        Data = data;
    }
}
