﻿using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class BillTest
    {
        [Fact]
        public void Create()
        {
            // When
            Bill b = new Bill();

            // Then
            Assert.NotEqual(Guid.Empty, b.Id);

            Assert.Null(b.Appointment);
            Assert.Equal(Guid.Empty, b.AppointmentId);
        }

        [Fact]
        public void AttachAppointment()
        {
            // Given
            Bill b = new Bill();
            var ap = new Appointment(10);

            // When
            b.AttachAppointment(ap);

            // Then
            Assert.Equal(ap.Id, b.AppointmentId);
            Assert.True(ReferenceEquals(ap, b.Appointment));
        }

        [Fact]
        public void CalculateBillPrice_WithoutPrescription()
        {
            // Given
            Bill b = new Bill();
            var apPrice = 10;
            var ap = new Appointment(apPrice);

            var expected = apPrice;

            // When
            b.CalculateBillPrice(ap);

            // Then
            Assert.Equal(expected, b.BillPrice);
        }

        [Fact]
        public void CalculateBillPrice_WithPrescriptionHavingDrugsAndProcedures()
        {
            // Given
            Bill b = new Bill();

            // Creating Prescription
            var pre = new Prescription("", "");

            // Creating PrescriptedDrug with 5 Drug of price equal to 15
            var drugPrice1 = 15;
            uint drugQuantity1 = 10;
            var drugPrice2 = 16;
            uint drugQuantity2 = 7;
            var pg1 = new PrescriptedDrug(drugQuantity1);
            pg1.AttachDrug(new Drug("", "", drugPrice1, 100));

            var pg2 = new PrescriptedDrug(drugQuantity2);
            pg2.AttachDrug(new Drug("", "", drugPrice2, 100));

            // Link PrescriptedDrugs to Prescription
            pre.RegisterPrescriptedDrugs(new List<PrescriptedDrug>() { pg1, pg2 });

            // Creating Procedures
            var proPrice1 = 55;
            var proPrice2 = 78;
            var pro1 = new Procedure("", "", proPrice1);
            var pro2 = new Procedure("", "", proPrice2);

            // Link Procedures to Prescription
            pre.RegisterProcedures(new List<Procedure> { pro1, pro2 });

            // Creating Appointment
            var apPrice = 70;
            var ap = new Appointment(apPrice);

            // Link Prescription to Appointment
            ap.RegisterPrescription(pre);

            var expected = apPrice +
                (drugPrice1 * drugQuantity1) + (drugPrice2 * drugQuantity2) +
                proPrice1 + proPrice2;

            // When
            b.CalculateBillPrice(ap);
            var actual = b.BillPrice;

            // Then
            Assert.Equal(expected, actual);
        }
    }
}
