using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class MedicalRoom
    {
        public MedicalRoom(string adress) 
        {
            this.Id = Guid.NewGuid(); 
            this.Adress = adress;
        }
        public Guid Id { get; private set; }
        public string Adress { get; private set; }
        public List<Doctor> Doctors { get; private set; } = new List<Doctor>();
        public DrugStock DrugStock { get; private set; }

        public Result RegisterDoctors(List<Doctor> doctors) 
        {
            if (!doctors.Any())
            {
                return Result.Failure("Add at least one doctor to the current MedicalRoom");
            }

            foreach (Doctor doctor in doctors)
            {
                doctor.AttachMedicalRoom(this);
                this.Doctors.Add(doctor);
            }

            return Result.Success();
        }
        
        public void RegisterDrugStock(DrugStock drugStock) 
        {
            this.DrugStock = drugStock;
        }
    }
}
