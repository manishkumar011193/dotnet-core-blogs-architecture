namespace dotnet_core_blogs_architecture.blogs.Service
{
    public interface ITokenService
    {
        Task<string> BuidToken(Data.Models.User user);  
    }
}
