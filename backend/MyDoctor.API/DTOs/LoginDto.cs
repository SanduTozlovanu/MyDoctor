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

        public DisplayLoginDto(Guid id, string accountType)
        {
            Id = id;
            AccountType = accountType;
        }
    }
}
