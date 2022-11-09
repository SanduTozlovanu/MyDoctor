using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDoctor.Bussiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.Bussiness.Entities.Tests
{
    [TestClass()]
    public class DoctorTests
    {
        [TestMethod()]
        public void GetFullNameTest()
        {

            //arrange
            Doctor doctor = new Doctor();
            doctor.FirstName = "Will";
            doctor.LastName = "Smith";
            string expected = "Will, Smith";
            //act
            string actual = doctor.FullName;
            Assert.AreEqual(expected, actual);
        }
    }
}