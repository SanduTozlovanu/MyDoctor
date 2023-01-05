namespace MyDoctor.Application.Response
{
    public class SurveyQuestionResponse
    {
        public SurveyQuestionResponse(string questionBody, string answer)
        {
            this.QuestionBody = questionBody;
            this.Answer = answer;
        }
        public string QuestionBody { get; set; }
        public string Answer { get; set; }
    }
}
