using FluentValidation;
using MyDoctor.Application.Commands.SurveyQuestionsCommands;

namespace MyDoctor.Application.Validators.SurveyQuestionsValidators
{
    public class UpdateSurveyQuestionsCommandValidator : AbstractValidator<UpdateSurveyQuestionsCommand>
    {
        private const string EMPTY_STRING_ERROR = "Empty string error!";

        public UpdateSurveyQuestionsCommandValidator()
        {
            RuleFor(sq => sq.HeadAcheAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.SexualAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.DiabetisAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.CancerAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.BloodPressureAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.HeadAcheAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.AllergiesAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
            RuleFor(sq => sq.CovidAnswer).MinimumLength(2).WithMessage(EMPTY_STRING_ERROR);
        }
    }
}
