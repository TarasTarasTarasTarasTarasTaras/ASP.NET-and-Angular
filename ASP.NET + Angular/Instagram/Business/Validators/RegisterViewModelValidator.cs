using Business.Models;
using Business.Services.Interfaces;
using FluentValidation;

namespace Business.Validators
{
    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidator(IUserService userService)
        {
            RuleFor(m => m.UserName)
                .Cascade(CascadeMode.Stop) 
                .NotEmpty()
                .WithMessage("UserName should be not empty.")
                .Must(s => !s.Contains(' '))
                .WithMessage("No spaces!")
                .Length(4, 20)
                .WithMessage("The length of the UserName should be between 4 and 20.")
                .Must(userName => userService.IsUserNameUnique(userName))
                .WithMessage("Username already taken")
                .Matches(@"^[a-zA-Z0-9]{4,20}$")
                .WithMessage("Bad username format");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .Must(email => userService.IsEmailUnique(email))
                .WithMessage("Email already taken");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter the password.")
                .Matches(@"^[a-zA-Z0-9]{6,32}$")
                .WithMessage("Bad password format");
        }
    }
}
