namespace MyDoctor.API.DTOs
{
    public class CreateDoctorDto
    {
        public CreateUserDto UserDetails { get; set; }
        public string Speciality { get; set; }
    }
    public class DisplayDoctorDto
    {
        public DisplayDoctorDto(Guid id, Guid medicalRoomId, string mail, string speciality, string firstName, string lastName) 
        {
            this.Id = id;
            this.MedicalRoomId= medicalRoomId;
            this.Mail = mail;
            this.FirstName= firstName;
            this.LastName= lastName;
            this.Speciality= speciality;
        }
        public Guid Id { get; set; }
        public Guid MedicalRoomId { get; set; }
        public string Mail { get; set; }
        public string Speciality { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
