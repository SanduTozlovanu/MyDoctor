﻿using FluentValidation;
using MyDoctor.API.DTOs;

namespace MyDoctor.API.Validations
{
    public class ProcedureValidator : AbstractValidator<CreateProcedureDto>
    {
        public const string NEGATIVE_PRICE_ERROR = "Price should not be negative.";
        public const string DESCRIPTION_LENGTH_ERROR = "The description should have between 10 and 400 characters";
        public const string NAME_LENGTH_ERROR = "The name should be between 5 and 120 characters";

        public ProcedureValidator()
        {
            RuleFor(p => p.Price).NotEmpty().GreaterThanOrEqualTo(0).WithMessage(NEGATIVE_PRICE_ERROR);
            RuleFor(p => p.Description).NotEmpty().Length(10, 400).WithMessage(DESCRIPTION_LENGTH_ERROR);
            RuleFor(p => p.Name).NotEmpty().Length(5, 120).WithMessage(NAME_LENGTH_ERROR);
        }
    }
}
