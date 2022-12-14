namespace MyDoctor.API.DTOs
{
    public class CreateDoctorDto
    {
        public CreateDoctorDto(CreateUserDto userDetails, Guid specialityId)
        {
            this.UserDetails = userDetails;
            SpecialityId = specialityId;
        }
        public CreateUserDto UserDetails { get; set; }
        public Guid SpecialityId { get; set; }
    }
    public class DisplayDoctorDto
    {
        public DisplayDoctorDto(Guid id, Guid medicalRoomId, Guid specialityId, string email, string firstName, string lastName)
        {
            this.Id = id;
            this.MedicalRoomId = medicalRoomId;
            this.SpecialityId = specialityId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public Guid Id { get; set; }
        public Guid MedicalRoomId { get; set; }
        public Guid SpecialityId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
