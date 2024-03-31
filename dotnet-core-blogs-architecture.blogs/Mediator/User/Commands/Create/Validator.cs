using dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Create;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.Data.Data;
using DT.Identity.Core.User.Commands.Shared;
using DT.Shared.Repository.Interfaces;
using FluentValidation;

namespace DT.Identity.Core.User.Commands.Create
{
    public class Validator : AbstractValidator<CommandModel>
    {       
        public Validator(IReadRepository<dotnet_core_blogs_architecture.Data.Models.User> userRepository) : base(userRepository)
        {              
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Please Enter Email in Correct Format.").MaximumLength(50).Custom(EmailIdValidator);
            RuleFor(x => x.Mobile).Custom(MobileValidator).When(x => !string.IsNullOrEmpty(x.Mobile));
        }

        private void EmailIdValidator(string email, ValidationContext<CommandModel> context)
        {
            if (this.userRepository.Any(new UserWithEmailSpecification(email)))
            {
                context.AddFailure("Email", "Email Already Exists!");
            }
        }

        private void MobileValidator(string mobile, ValidationContext<CommandModel> context)
        {
            if (userRepository.Any(new UserWithMobileSpecification(mobile)))
            {
                context.AddFailure("Mobile", "Mobile Already Exists!");
            }
        }
    }
}
