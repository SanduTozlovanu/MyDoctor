﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Commands.SurveyQuestionsCommands;
using MyDoctor.Application.Queries.SurveyQuestionsQueries;
using MyDoctor.Application.Responses;
using MyDoctor.Application.Validators.SurveyQuestionsValidators;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SurveyQuestionsController : ControllerBase
    {
        private readonly IMediator mediator;

        public SurveyQuestionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{patientId:guid}")]
        public async Task<IActionResult> Get(Guid patientId)
        {
            var result = await mediator.Send(new GetPatientSurveyQuestionsQuery(patientId));
            return result.Count > 0 && !result[0].IsStatusOk() ? result[0].GetStatusResult() : (IActionResult)Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult<List<SurveyQuestionResponse>>> Update([FromBody] UpdateSurveyQuestionsCommand command)
        {
            UpdateSurveyQuestionsCommandValidator validator = new();
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0].ErrorMessage);
            }
            var result = await mediator.Send(command);
            return result.Count > 0 && !result[0].IsStatusOk() ? (ActionResult<List<SurveyQuestionResponse>>)result[0].GetStatusResult() : (ActionResult<List<SurveyQuestionResponse>>)Ok(result);
        }
    }
}
