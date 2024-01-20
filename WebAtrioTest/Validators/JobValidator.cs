using FluentValidation;
using WebAtrioTest.ViewModels;

namespace WebAtrioTest.Validators;

public class JobValidator: AbstractValidator<JobViewModel>
{
    public JobValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty();
        RuleFor(x => x.Organisation).NotEmpty();
        RuleFor(x => x.Position).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
            .WithMessage("The end date must be greater than the start date");
    }
}
