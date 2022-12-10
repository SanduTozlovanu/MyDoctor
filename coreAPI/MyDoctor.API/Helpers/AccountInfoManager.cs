using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace MyDoctor.API.Helpers
{
    public static class AccountInfoManager
    {
        public static string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.UTF8.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashedPassword);
        }

        public static bool ValidatePassword(string hashedPassword, string password)
        {
            string newHashedPassword = HashPassword(password);

            return hashedPassword.Equals(newHashedPassword);
        }

        public static bool ValidateEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email) && !string.IsNullOrEmpty(email);
        }
    }

}
