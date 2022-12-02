namespace MyDoctor.API.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    

    }

    class DisplayLoginDto
    {
        public Guid Id { get; set; }
        public string AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string JwtToken { get; set; }

        public DisplayLoginDto(Guid id, string accountType, string firstName, string lastName, string email, string jwtToken)
        {
            Id = id;
            AccountType = accountType;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            JwtToken = jwtToken;
        }
    }
}
