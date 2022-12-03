namespace MyDoctor.API.DTOs
{
    public class CreateUserDto
    {
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
