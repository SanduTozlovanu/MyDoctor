using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class MedicalRoom
    {
        public MedicalRoom(string adress)
        {
            Id = Guid.NewGuid();
            Adress = adress;
            Doctors = new List<Doctor>();
        }
        public Guid Id { get; private set; }
        public string Adress { get; private set; }
        public virtual List<Doctor> Doctors { get; private set; }
        public virtual DrugStock DrugStock { get; private set; }

        public Result RegisterDoctors(List<Doctor> doctors)
        {
            if (!doctors.Any())
            {
                return Result.Failure("Add at least one doctor to the current MedicalRoom");
            }

            foreach (Doctor doctor in doctors)
            {
                doctor.AttachToMedicalRoom(this);
                Doctors.Add(doctor);
            }

            return Result.Success();
        }

        public void RegisterDrugStock(DrugStock drugStock)
        {
            drugStock.AttachToMedicalRoom(this);
            DrugStock = drugStock;

        }
    }
}
