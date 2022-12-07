using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class MedicalRoomTest
    {
        private const string ADRESS = "ads";

        [Fact]
        public void Create()
        {
            // When
            var mr = new MedicalRoom(ADRESS);

            // Then
            Assert.NotEqual(Guid.Empty, mr.Id);
            Assert.Equal(ADRESS, mr.Adress);

            Assert.Null(mr.DrugStock);
            Assert.Empty(mr.Doctors);
        }

        [Fact]
        public void RegisterDrugStock()
        {
            // Given
            var mr = new MedicalRoom(ADRESS);
            var ds = new DrugStock();

            // When
            mr.RegisterDrugStock(ds);

            // Then
            Assert.Equal(mr.Id, ds.MedicalRoomId);
            Assert.True(ReferenceEquals(mr, ds.MedicalRoom));
            Assert.True(ReferenceEquals(ds, mr.DrugStock));
        }

        [Fact]
        public void RegisterDoctors_EmptyParameter()
        {
            // Given
            var mr = new MedicalRoom(ADRESS);

            // When
            var actual = mr.RegisterDoctors(new List<Doctor>());

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void RegisterDoctors_Valid()
        {
            // Given
            var mr = new MedicalRoom(ADRESS);

            // When
            var actual = mr.RegisterDoctors(new List<Doctor>()
            {
                DoctorTest.CreateDefaultDoctor()
            });

            // Then
            Assert.True(actual.IsSuccess);
        }
    }
}
