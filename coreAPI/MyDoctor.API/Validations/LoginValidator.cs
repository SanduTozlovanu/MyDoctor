using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDoctor.API.Validations
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public const string WRONG_EMAIL_FORMAT_ERROR = "The email format is invalid";
        public const string PASSWORD_LENGTH_ERROR = "The password should contain between 7 and 20 characters";
        public LoginValidator()
        {
            RuleFor(l => l.Email).NotEmpty().EmailAddress().WithMessage(WRONG_EMAIL_FORMAT_ERROR);
            RuleFor(l => l.Password).NotEmpty().Length(7, 20).WithMessage(PASSWORD_LENGTH_ERROR);
        }
    }
}
