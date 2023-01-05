namespace MyDoctorApp.Domain.Models
{
    public class Speciality
    {
        public Speciality(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Doctors = new List<Doctor>();
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public virtual List<Doctor> Doctors { get; private set; }

        public void RegisterDoctor(Doctor doctor)
        {
            doctor.AttachToSpeciality(this);
            Doctors.Add(doctor);
        }
    }
}
