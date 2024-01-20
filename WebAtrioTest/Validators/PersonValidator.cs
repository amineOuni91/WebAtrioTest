using FluentValidation;
using WebAtrioTest.ViewModels;

namespace WebAtrioTest.Validators;

public class PersonValidator: AbstractValidator<PersonViewModel>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.BirthDate).NotEmpty().Must(x => CalculateAge(x) < 150)
            .WithMessage("The age must be under or equal to 150 years");
    }

    private static int CalculateAge(DateTime birthDate)
    {
        var age = DateTime.Today.Year - birthDate.Year;
        if (birthDate.Date > DateTime.Today.AddYears(-age)) age--;
        return age;
    }
}