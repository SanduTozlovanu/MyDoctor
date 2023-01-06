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
        public DisplayDoctorDto(Guid id, Guid medicalRoomId, Guid specialityId, string email, string firstName, //NOSONAR
            string lastName, uint appointmentPrice, string description, string username)
        {
            this.Id = id;
            this.MedicalRoomId = medicalRoomId;
            this.SpecialityId = specialityId;
            this.Email = email;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.AppointmentPrice = appointmentPrice;
            this.Description = description;
            this.Username = username;
        }
        public Guid Id { get; set; }
        public Guid MedicalRoomId { get; set; }
        public Guid SpecialityId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint AppointmentPrice { get; set; }
        public string Description { get; }
        public string Username { get; }
    }
    public class DisplayDoctorWithSpecialityDto
    {
        public DisplayDoctorWithSpecialityDto(DisplayDoctorDto displayDoctorDto, string speciality)
        {
            this.Id = displayDoctorDto.Id;
            this.MedicalRoomId = displayDoctorDto.MedicalRoomId;
            this.SpecialityId = displayDoctorDto.SpecialityId;
            this.Speciality = speciality;
            this.Email = displayDoctorDto.Email;
            this.FirstName = displayDoctorDto.FirstName;
            this.LastName = displayDoctorDto.LastName;
            this.AppointmentPrice = displayDoctorDto.AppointmentPrice;
            this.Description = displayDoctorDto.Description;
            this.Username = displayDoctorDto.Username;
        }
        public Guid Id { get; set; }
        public Guid MedicalRoomId { get; set; }
        public Guid SpecialityId { get; set; }
        public string Speciality { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public uint AppointmentPrice { get; set; }
        public string Description { get; }
        public string Username { get; }
    }
    public class DisplayDoctorPhotoDto
    {
        public IFormFile ProfilePhoto { get; set; }
    }
    public class UpdateDoctorPhotosDto
    {
        public IFormFile ProfilePhoto { get; set; }
        public IFormFile DiplomaPhoto { get; set; }
    }
    public class UpdateDoctorDto
    {
        public UpdateDoctorDto(UpdateUserDto updateUserDto, uint appointmentPrice)
        {
            this.UpdateUserDto = updateUserDto;
            this.AppointmentPrice = appointmentPrice;
        }
        public UpdateUserDto UpdateUserDto { get; set; }
        public uint AppointmentPrice { get; set; }
    }

}
