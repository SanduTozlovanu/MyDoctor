using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class DrugStock
    {
        public DrugStock()
        {
            this.Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public List<Drug> Drugs { get; private set; } = new List<Drug>();
        public MedicalRoom MedicalRoom { get; private set; }
        public Guid MedicalRoomId { get; private set; }

        public Result RegisterDrugsToDrugStock(List<Drug> drugs)
        {
            if (!drugs.Any())
            {
                return Result.Failure("Add at least one drug for the current DrugStock");
            }


            foreach (Drug drug in drugs)
            {
                drug.AttachDrugStock(this);
                Drugs.Add(drug);
            }

            return Result.Success();
        }

        public void AttachMedicalRoom(MedicalRoom medicalRoom) 
        {
            this.MedicalRoomId = medicalRoom.Id;
            this.MedicalRoom = medicalRoom;
        }

    }
}
