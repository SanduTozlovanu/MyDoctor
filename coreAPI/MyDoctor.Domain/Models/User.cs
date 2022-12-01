using MyDoctor.Domain.Models;
using MyDoctorApp.Domain.Helpers;
using System.ComponentModel.DataAnnotations;

namespace MyDoctor
{
    public class User
    {
        public User(string accountType, string email, string password, string firstName, string lastName)
        {
            this.Id = Guid.NewGuid();
            this.AccountType = accountType;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.Password = password;
            this.LastName = lastName;
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
    }
}