using DataApi.Shared.Models;
using FluentValidation;

namespace WebAssemblyApp.Validators
{
    public class PlanValidator : AbstractValidator<Plan>
    {
        public PlanValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty();
            RuleFor(p => p.Description)
                .NotEmpty();
        }
    }
}
