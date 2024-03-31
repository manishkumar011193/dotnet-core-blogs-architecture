using dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Shared;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.infrastructure.Interfaces;
using FluentValidation;

namespace dotnet_core_blogs_architecture.blogs.Mediator.User.Commands.Create
{
    public class Validator : UserBaseValidator<CommandModel>
    {       
        public Validator(IReadRepository<Data.Models.User> userRepository) : base(userRepository)
        {              
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Please Enter Email in Correct Format.").MaximumLength(50).Custom(EmailIdValidator);            
        }

        private void EmailIdValidator(string email, ValidationContext<CommandModel> context)
        {
            if (this.userRepository.Any(new UserWithEmailSpecification(email)))
            {
                context.AddFailure("Email", "Email Already Exists!");
            }
        }       
    }
}
