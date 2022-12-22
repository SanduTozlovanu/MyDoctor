using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Queries.MedicalRoomQueries;
using MyDoctor.Application.Responses;

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
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
