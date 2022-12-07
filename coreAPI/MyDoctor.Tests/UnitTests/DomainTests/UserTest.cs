using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class UserTest
    {
        private const string EMAIL = "ceva@gmail.com";
        private const string PASSWORD = "pass";
        private const string FIRST_NAME = "Ionut";
        private const string LAST_NAME = "Virgil";

        private Patient CreateDefaultPatient()
        {
            var email = EMAIL;
            var password = PASSWORD;
            var firstName = FIRST_NAME;
            var lastName = LAST_NAME;
            uint age = 10;

            return new Patient(email, password, firstName, lastName, age);
        }

        [Fact]
        public void Create()
        {
            // When
            User u = CreateDefaultPatient();

            // Then
            Assert.NotNull(u);
            Assert.True(u.Id != Guid.Empty);
            Assert.Equal(EMAIL, u.Email);
            Assert.Equal(PASSWORD, u.Password);
            Assert.Equal(FIRST_NAME, u.FirstName);
            Assert.Equal(LAST_NAME, u.LastName);
            Assert.Contains(u.AccountType, new List<String>(){AccountTypes.Admin, AccountTypes.Patient, AccountTypes.Doctor});
        }

        [Fact]
        public void ValidateFullname()
        {
            // Given
            User u = CreateDefaultPatient();
            var expected = "Ionut, Virgil";

            // When
            var actual = u.FullName;

            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Update()
        {
            // Given
            User u = CreateDefaultPatient();
            string accType = "type";
            string email = "email";
            string password = "password";
            string firstName = "firstName";
            string lastName = "lastName";

            User newUser = new User(accType, email, password, firstName, lastName);

            // When
            u.Update(newUser);

            // Then
            Assert.Equal(newUser.Email, u.Email);
            Assert.Equal(newUser.Password, u.Password);
            Assert.Equal(newUser.FirstName, u.FirstName);
            Assert.Equal(newUser.LastName, u.LastName);
        }
    }
}
