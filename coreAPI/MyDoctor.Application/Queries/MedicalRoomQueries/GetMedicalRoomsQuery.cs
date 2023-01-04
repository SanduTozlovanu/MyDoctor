using MediatR;
using MyDoctor.Application.Responses;

namespace MyDoctor.Application.Queries.MedicalRoomQueries
{
    public class GetMedicalRoomsQuery : IRequest<List<MedicalRoomResponse>>{}
}
