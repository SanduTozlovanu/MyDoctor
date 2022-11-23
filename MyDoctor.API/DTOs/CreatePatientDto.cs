namespace MyDoctor.API.DTOs
{
    public class CreatePatientDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public uint Age { get; set; }
        public string Password { get; set; }

    }
}
