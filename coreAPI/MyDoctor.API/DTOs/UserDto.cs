namespace MyDoctor.API.DTOs
{
    public class CreateUserDto
    {
        public CreateUserDto(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public string Email { get; set; }
        public string Password { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class DisplayUserDto
    {
        public DisplayUserDto(User user)
        {
            Id = user.Id;
            AccountType = user.AccountType;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public Guid Id { get; set; }
        public string AccountType { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
