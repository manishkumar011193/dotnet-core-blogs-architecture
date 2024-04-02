namespace dotnet_core_blogs_architecture.Data.Results;

public class InvalidValidationResult : ValidationResult
{
	public InvalidValidationResult(string field, string description)
	{
		AddError(field, description);
	}
}
