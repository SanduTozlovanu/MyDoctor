using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDoctor.API.Validations
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public const string PASSWORD_LENGTH_ERROR = "The password should contain between 7 and 20 characters";
        public const string INVALID_MAIL_ERROR = "Invalid mail format";
        public const string EMPTY_FIRSTNAME_ERROR = "FirstName cannot be empty";
        public const string EMPTY_LASTNAME_ERROR = "The lastname cannot be empty";

        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage(EMPTY_FIRSTNAME_ERROR);
            RuleFor(u => u.LastName).NotEmpty().WithMessage(EMPTY_LASTNAME_ERROR);
            RuleFor(u => u.Email).NotEmpty().EmailAddress().WithMessage(INVALID_MAIL_ERROR);
            RuleFor(u => u.Password).NotEmpty().Length(7, 20).WithMessage(PASSWORD_LENGTH_ERROR);
        }
    }
}
