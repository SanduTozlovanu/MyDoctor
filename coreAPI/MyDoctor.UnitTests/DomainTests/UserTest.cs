using MyDoctor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.UnitTests.DomainTests
{
    public class UserTest
    {
        private Patient CreateDefaultPatient()
        {
            var email = "ceva@gmail.com";
            var password = "pass";
            var firstName = "Ionut";
            var lastName = "Virgil";
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
