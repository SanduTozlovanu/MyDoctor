﻿namespace MyDoctor.Application.Responses
{
    public class SurveyQuestionResponse
    {
        public SurveyQuestionResponse(string questionBody, string answer)
        {
            QuestionBody = questionBody;
            Answer = answer;
        }
        public string QuestionBody { get; set; }
        public string Answer { get; set; }
    }
}
