using DataApi.Shared.Models;
using FluentValidation;

namespace DataApi.Shared.Validators
{
    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Login).NotEmpty();
        }
    }
}
