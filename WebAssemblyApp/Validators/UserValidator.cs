using DataApi.Shared.Models;
using FluentValidation;

namespace WebAssemblyApp.Validators
{
    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Password must at least 5 characters");
            
            RuleFor(x => x.Login)
                .NotEmpty();
            
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email in not a validemail address");
        }
    }
}
