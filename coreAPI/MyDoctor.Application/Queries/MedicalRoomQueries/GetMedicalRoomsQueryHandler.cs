using MediatR;
using MyDoctor.Application.Mappers.MedicalRoomMappers;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.Application.Queries.MedicalRoomQueries
{
    public class GetMedicalRoomsQueryHandler :
        IRequestHandler<GetMedicalRoomsQuery,
            List<MedicalRoomResponse>>
    {
        private readonly IRepository<MedicalRoom> repository;

        public GetMedicalRoomsQueryHandler(IRepository<MedicalRoom> repository)
        {
            this.repository = repository;
        }
        public async Task<List<MedicalRoomResponse>> Handle(GetMedicalRoomsQuery request, CancellationToken cancellationToken)
        {
            var medicalRooms = (await repository.AllAsync()).ToList();
            return AvailableAppointmentIntervalsMapper.Mapper.Map<List<MedicalRoomResponse>>(medicalRooms);
        }
    }
}
