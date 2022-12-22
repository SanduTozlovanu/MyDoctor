namespace MyDoctor.API.DTOs
{
    public class CreatePatientDto
    {
        public CreatePatientDto(CreateUserDto userDetails, uint age)
        {
            this.UserDetails = userDetails;
            this.Age = age;
        }
        public CreateUserDto UserDetails { get; set; }
        public uint Age { get; set; }
    }
    public class DisplayPatientDto
    {
        public DisplayPatientDto(Guid id, string firstName, string lastName, string email, uint age, string description, string username)
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Email = email;
            this.Age = age;
            Description = description;
            Username = username;
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public uint Age { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
    }
}
