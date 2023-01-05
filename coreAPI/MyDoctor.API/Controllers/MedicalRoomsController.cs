using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Queries.MedicalRoomQueries;
using MyDoctor.Application.Responses;
using MyDoctor.Application.Validators.MedicalRoomValidators;

namespace MyDoctor.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class MedicalRoomsController : ControllerBase
    {
        private readonly IMediator mediator;

        public MedicalRoomsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetMedicalRoomsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<MedicalRoomResponse>>> Create([FromBody] CreateMedicalRoomCommand command)
        {
            CreateMedicalRoomCommandValidator validator = new();
            var validationResult = validator.Validate(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors[0].ErrorMessage);
            }
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
