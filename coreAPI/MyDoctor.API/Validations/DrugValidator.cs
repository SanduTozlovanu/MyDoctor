using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDocAppointment.API.Validations
{
    public class CreateDrugValidator : AbstractValidator<CreateDrugDto>
    {
        public const string NEGATIVE_PRICE_ERROR = "Price should not be negative.";
        public const string DESCRIPTION_LENGTH_ERROR = "The description should have between 10 and 400 characters";
        public const string NAME_LENGTH_ERROR = "The name should be between 5 and 120 characters";
        public const string NEGATIVE_QUANTITY_ERROR = "Quantity should not be negative.";
        public CreateDrugValidator()
        {
            RuleFor(d => d.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage(NEGATIVE_PRICE_ERROR);
            RuleFor(d => d.Description).NotEmpty().Length(10, 400).WithMessage(DESCRIPTION_LENGTH_ERROR);
            RuleFor(d => d.Name).NotEmpty().Length(5, 120).WithMessage(NAME_LENGTH_ERROR);
            RuleFor(d => d.Quantity).NotNull().Must(IsQuantityValid).WithMessage(NEGATIVE_QUANTITY_ERROR);
        }

        static public bool IsQuantityValid(uint quantity)
        {
            if (quantity <= 0) return false;
            return true;
        }
    }

    public class GetDrugValidator : AbstractValidator<GetDrugDto>
    {
        private const string NEGATIVE_QUANTITY_ERROR = "Quantity should not be negative.";
        public GetDrugValidator()
        {
            RuleFor(d => d.drugId).NotEmpty();
            RuleFor(d => d.Quantity).NotNull().Must(CreateDrugValidator.IsQuantityValid).WithMessage(NEGATIVE_QUANTITY_ERROR);
        }
    }
}
