namespace MyDoctor.API.DTOs
{
    public class CreateDoctorDto
    {
        public string Mail { get; set; }
        public string Password { get;  set; }
        public string Speciality { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
