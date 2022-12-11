using System;
namespace MyDoctorApp.Domain.Models
{
    public class User
    {
        public User(string accountType, string email, string password, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            AccountType = accountType;
            Email = email;
            Password = password;
            FirstName = firstName;
            Password = password;
            LastName = lastName;
        }

        private const string SEPARATOR = ", ";
        public Guid Id { get; private set; }
        public string AccountType { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName
        {
            get { return $"{FirstName}{SEPARATOR}{LastName}"; }
        }

        public void Update(User user)
        {
            Email = user.Email;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }

        public static string CreateRandomEmail()
        {
            return string.Format("{0}@{1}.com", GenerateRandomAlphabetString(10), GenerateRandomAlphabetString(10));
        }
        private static string GenerateRandomAlphabetString(int length)
        {
            string allowedChars = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var rnd = new Random(Guid.NewGuid().GetHashCode());

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rnd.Next(allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}