using MyDoctor.Domain.Models;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests
{
    [TestClass]
    public class DomainTest
    {
        [TestMethod]
        public void DoctorTest()
        {
            string firstName = "Andrei";
            string lastName = "Ciobanu";
            Doctor doctor = new Doctor("mail@gmail.com", "parola", firstName, lastName, "specialitate");
            
            string expected = firstName+ ", " + lastName;
            string actual = doctor.GetFullName();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void PatientTest()
        {
            string firstName = "Andrei";
            string lastName = "Ciobanu";
            Patient patient = new Patient("mail@gmail.com", "parola", firstName, lastName, 15);

            string expected = firstName + ", " + lastName;
            string actual = patient.GetFullName();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void DrugTest()
        {
            Drug drug = new Drug("paracetamol", "standard", 2, 1);

            bool expected = true;
            bool actual = drug.ConsumeDrug().IsSuccess;

            Assert.AreEqual(expected, actual);


            expected = true;
            actual = drug.ConsumeDrug().IsFailure;

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void BillingTest()
        {
            Appointment appointment = new Appointment(50);
            Prescription prescription = new Prescription("test", "description");
            Drug drug1 = new Drug("paracetamol", "pentru raceala", 10, 5);
            Drug drug2 = new Drug("nurofen", "pentru cap", 15, 10);
            List<Drug> drugs = new List<Drug> { drug1, drug2 };
            Bill bill = new Bill();

            prescription.RegisterDrugs(drugs);
            appointment.RegisterBill(bill);
            appointment.RegisterPrescription(prescription);

            appointment.CalculateBillPrice();

            double expected = appointment.Price + (drug1.Price * drug1.Quantity) + (drug2.Price * drug2.Quantity);
            double actual = appointment.Bill.BillPrice;

            Assert.AreEqual(expected, actual);
        }
    }
}