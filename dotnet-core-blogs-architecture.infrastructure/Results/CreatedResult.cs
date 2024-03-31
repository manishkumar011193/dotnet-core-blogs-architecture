using System;
using System.Collections.Generic;
using System.Text;

namespace dotnet_core_blogs_architecture.Data.Results;

public class CreatedResult : ValidationResult
{
	public dynamic Data { get; private set; }

	public CreatedResult(dynamic data)
	{
		Data = data;
	}
}
