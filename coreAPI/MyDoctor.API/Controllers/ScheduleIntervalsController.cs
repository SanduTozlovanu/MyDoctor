﻿using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Commands.ScheduleIntervalCommands;
using MyDoctor.Application.Queries.ScheduleIntervalQueries;
using MyDoctor.Application.Responses;
using MyDoctor.Application.Validators.ScheduleIntervalValidators;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ScheduleIntervalsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ScheduleIntervalsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <remarks>
        /// Request Example:
        /// 
        ///     {
        ///         "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///         "startTime": "15:00",
        ///         "endTime": "15:30"
        ///     }
        ///     
        ///         
        /// </remarks>
        [HttpPut]
        public async Task<ActionResult<List<ScheduleIntervalResponse>>> Update([FromBody] List<UpdateScheduleIntervalDto> scheduleIntervalList)
        {
            UpdateMedicalRoomCommandValidator validator = new();
            UpdateScheduleIntervalCommand command = new(scheduleIntervalList);
            ValidationResult validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0].ErrorMessage);
            }
            var result = await mediator.Send(command);
            return result.Count > 0 && !result[0].IsStatusOk() ? (ActionResult<List<ScheduleIntervalResponse>>)result[0].GetStatusResult() : (ActionResult<List<ScheduleIntervalResponse>>)Ok(result);
        }

        [HttpGet("{doctorId:guid}")]
        public async Task<ActionResult<List<ScheduleIntervalResponse>>> Get(Guid doctorId)
        {
            var result = await mediator.Send(new GetDoctorScheduleIntervalsQuery(doctorId));
            return Ok(result);
        }
    }
}
