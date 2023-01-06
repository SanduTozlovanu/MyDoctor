using FluentValidation;
using MyDoctor.Application.Commands.SurveyQuestionsCommands;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;

namespace MyDoctor.Application.Validators.SurveyQuestionsValidators
{
    public class UpdateSurveyQuestionsCommandValidator : AbstractValidator<UpdateSurveyQuestionsCommand>
    {
        private const string WRONG_QUESTIONLIST_ERROR = "QuestionList is of wrong format." +
            " It might have too short answers or have different questions from the database";

        public UpdateSurveyQuestionsCommandValidator()
        {
            RuleFor(sq => sq.QuestionList).Must(IsQuestionListValid).WithMessage(WRONG_QUESTIONLIST_ERROR);
        }

        public static bool IsQuestionListValid(List<SurveyQuestionResponse> questionList)
        {
            if (questionList.Count != Enum.GetValues(typeof(SurveyQuestion.Question)).Length)
            {
                return false;
            }
            bool isValid = true;
            questionList.ForEach(question =>
            {
                if (!IsQuestionBodyValid(question.QuestionBody))
                    isValid = false;
                if (question.Answer.Length < 2)
                    isValid = false;
            });
            return isValid;
        }
        public static bool IsQuestionBodyValid(string questionBody)
        {
            foreach (SurveyQuestion.Question question in Enum.GetValues(typeof(SurveyQuestion.Question)))
            {
                if (SurveyQuestion.GetQuestionBody(question) == questionBody)
                    return true;
            }
            return false;
        }
    }
}
