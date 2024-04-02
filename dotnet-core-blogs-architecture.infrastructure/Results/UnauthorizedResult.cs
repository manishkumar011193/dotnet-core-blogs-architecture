namespace dotnet_core_blogs_architecture.Data.Results;

public class UnauthorizedResult : ValidationResult
{
    public UnauthorizedResult(string field, string description)
    {
        this.AddError(field, description);
    }
}