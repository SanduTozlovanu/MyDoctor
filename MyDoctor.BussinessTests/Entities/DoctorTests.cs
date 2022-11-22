using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDoctor.Domain.Models;

namespace MyDoctor.Bussiness.Entities.Tests
{
    [TestClass()]
    public class DoctorTests
    {
        [TestMethod()]
        public void GetFullNameTest()
        {

            //arrange
            Doctor doctor = new Doctor("troller@gmail.com", "064845", "Ion","Puscasu","Chirurg");
            string expected = "Ion, Puscasu";
            //act
            string actual = doctor.FullName;
            Assert.AreEqual(expected, actual);
        }
    }
}