using MyDoctor.Domain.Models;
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
            User p = CreateDefaultPatient();

            // Then
            Assert.NotNull(p);
            Assert.True(p.Id != Guid.Empty);
            Assert.Equal(EMAIL, p.Email);
            Assert.Equal(PASSWORD, p.Password);
            Assert.Equal(FIRST_NAME, p.FirstName);
            Assert.Equal(LAST_NAME, p.LastName);
            Assert.Contains(p.AccountType, new List<String>(){AccountTypes.Admin, AccountTypes.Patient, AccountTypes.Doctor});
        }

        [Fact]
        public void ValidateFullname()
        {
            // Given
            User p = CreateDefaultPatient();
            var expected = "Ionut, Virgil";

            // When
            var actual = p.FullName;

            // Then
            Assert.Equal(expected, actual);
        }
    }
}
