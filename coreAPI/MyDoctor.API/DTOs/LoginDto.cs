namespace MyDoctor.API.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    class DisplayLoginDto
    {
        public DisplayUserDto UserDetails { get; set; }
        public string JwtToken { get; set; }

        public DisplayLoginDto(User user, string jwtToken)
        {
            UserDetails = new DisplayUserDto(user);
            JwtToken = jwtToken;
        }
    }
}
