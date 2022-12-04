using MyDoctor.Domain.Models;

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
            Doctor doctor = new Doctor("email@gmail.com", "parola", firstName, lastName, "specialitate");
            
            string expected = firstName+ ", " + lastName;
            //string actual = doctor.GetFullName();

            //Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void PatientTest()
        {
            string firstName = "Andrei";
            string lastName = "Ciobanu";
            Patient patient = new Patient("email@gmail.com", "parola", firstName, lastName, 15);

            string expected = firstName + ", " + lastName;
            //string actual = patient.GetFullName();

            //Assert.AreEqual(expected, actual);

        }

        //[TestMethod]
        //public void DrugTest()
        //{
        //    Drug drug = new Drug("paracetamol", "standard", 2, 1);

        //    bool expected = true;
        //    bool actual = drug.ConsumeDrug().IsSuccess;

        //    Assert.AreEqual(expected, actual);


        //    expected = true;
        //    actual = drug.ConsumeDrug().IsFailure;

        //    Assert.AreEqual(expected, actual);

        //}

        [TestMethod]
        public void BillingTest()
        {
            Appointment appointment = new Appointment(50);
            Prescription prescription = new Prescription("test", "description");
            Drug drug1 = new Drug("paracetamol", "pentru raceala", 10, 5);
            Drug drug2 = new Drug("nurofen", "pentru cap", 15, 10);
            Procedure procedure1 = new Procedure("Masaj", "Faceti masaj la picioare atent", 260);
            Procedure procedure2 = new Procedure("Amputare", "Faceti amputare la picioare atent", 50);
            List<Procedure> procedures = new List<Procedure> { procedure1, procedure2 };
            List<Drug> drugs = new List<Drug> { drug1, drug2 };
            List<PrescriptedDrug> prescriptedDrugs = new List<PrescriptedDrug>();
            drugs.ForEach(drug =>
            {
                PrescriptedDrug prescriptedDrug = new PrescriptedDrug(drug.Quantity);
                prescriptedDrug.AttachDrug(drug);
                prescriptedDrugs.Add(prescriptedDrug);
            });

            prescription.RegisterPrescriptedDrugs(prescriptedDrugs);
            prescription.RegisterProcedures(procedures);
            appointment.RegisterPrescription(prescription);
            Bill bill = new Bill();
            appointment.RegisterBill(bill);


            double expected = appointment.Price
               + (drug1.Price * drug1.Quantity) 
               + (drug2.Price * drug2.Quantity)
               + procedure1.Price + procedure2.Price;
            double actual = appointment.Bill.BillPrice;
            Console.WriteLine(expected);
            Console.WriteLine(actual);

            Assert.AreEqual(expected, actual);
        }
    }
}