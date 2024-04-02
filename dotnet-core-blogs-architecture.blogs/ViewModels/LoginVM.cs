using System.ComponentModel.DataAnnotations;

namespace dotnet_core_blogs_architecture.blogs.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool? RememberMe { get; set; }
    }
}
