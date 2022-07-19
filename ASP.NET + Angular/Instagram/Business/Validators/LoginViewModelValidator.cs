using Business.Models;
using FluentValidation;

namespace Business.Validators
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(m => m.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("UserName should be not empty.")
                .Must(s => !s.Contains(' '))
                .WithMessage("No spaces!")
                .Length(4, 20)
                .WithMessage("The length of the UserName should be between 4 and 20.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter the password.")
                .Matches(@"^[a-zA-Z0-9]{6,32}$")
                .WithMessage("Bad password format.");
        }
    }
}
