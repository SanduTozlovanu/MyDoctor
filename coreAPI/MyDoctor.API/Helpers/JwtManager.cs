using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyDoctor.API.Helpers
{
    public static class JwtManager
    {

        private static string SECRET = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        private static SecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));
        private static string ISSUER = "issuer";
        private static string AUDIENCE = "audience";

        public static string GenerateToken(User user, int expireAfterHours = 24)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Typ, user.AccountType)
            };

            var signingCredentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256Signature);


            var now = DateTime.UtcNow;
            var expires = now.AddHours(Convert.ToInt32(expireAfterHours));

            var token = new JwtSecurityToken(
                ISSUER,
                AUDIENCE,
                claims,
                null,
                expires,
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public static bool ValidateCurrentToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = ISSUER,
                    ValidAudience = AUDIENCE,
                    IssuerSigningKey = symmetricSecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
