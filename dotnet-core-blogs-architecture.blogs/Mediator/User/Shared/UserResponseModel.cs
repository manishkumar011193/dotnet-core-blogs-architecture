namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Shared
{
    public class UserResponseModel
    {
        public long Id { get; set; }    
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public bool IsActive { get; set; }

        public string PasswordHash { get; set; }
    }
}
