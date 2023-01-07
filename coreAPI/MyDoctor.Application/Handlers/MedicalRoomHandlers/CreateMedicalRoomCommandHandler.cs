using MediatR;
using MyDoctor.Application.Commands.MedicalRoomCommands;
using MyDoctor.Application.Exceptions;
using MyDoctor.Application.Mappers.MedicalRoomMappers;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;

namespace MyDoctor.Application.Handlers.MedicalRoomHandlers
{
    public class CreateMedicalRoomCommandHandler : IRequestHandler<CreateMedicalRoomCommand, MedicalRoomResponse>
    {
        private readonly IRepository<MedicalRoom> medicalRoomRepository;
        private readonly IRepository<DrugStock> drugStockRepository;

        public CreateMedicalRoomCommandHandler(IRepository<MedicalRoom> medicalRoomRepository, IRepository<DrugStock> drugStockRepository)
        {
            this.medicalRoomRepository = medicalRoomRepository;
            this.drugStockRepository = drugStockRepository;
        }
        public async Task<MedicalRoomResponse> Handle(CreateMedicalRoomCommand request, CancellationToken cancellationToken)
        {
            MedicalRoom medicalRoomEntity = MedicalRoomMapper.Mapper.Map<MedicalRoom>(request);
            if (medicalRoomEntity == null)
            {
                throw new BaseMapperException();
            }
            var drugStock = new DrugStock();
            medicalRoomEntity.RegisterDrugStock(drugStock);
            var newMedicalRoomEntity = await medicalRoomRepository.AddAsync(medicalRoomEntity);
            await drugStockRepository.AddAsync(drugStock);
            await medicalRoomRepository.SaveChangesAsync();
            await drugStockRepository.SaveChangesAsync();
            return MedicalRoomMapper.Mapper.Map<MedicalRoomResponse>(newMedicalRoomEntity);
        }
    }
}
