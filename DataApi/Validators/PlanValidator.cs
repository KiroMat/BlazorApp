using DataApi.Shared.Models;
using FluentValidation;

namespace DataApi.Validators
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
