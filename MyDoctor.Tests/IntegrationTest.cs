using MyDoctor.Domain.Models;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void PatientsController()
        {
            string firstName = "Andrei";
            string lastName = "Ciobanu";
            Doctor doctor = new Doctor("mail@gmail.com", "parola", firstName, lastName, "specialitate");
            
            string expected = firstName+ ", " + lastName;
            string actual = doctor.GetFullName();

            Assert.AreEqual(expected, actual);

        }
    }
}