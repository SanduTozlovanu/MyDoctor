using MediatR;
using MyDoctor.Application.Response;
using MyDoctor.Application.Responses;

namespace MyDoctor.Application.Commands.MedicalRoomCommands
{
    public  class CreateMedicalRoomCommand : IRequest<MedicalRoomResponse>
    {
        public CreateMedicalRoomCommand(string adress)
        {
            Adress = adress;
        }

        public string Adress { get; set; }
    }
}
