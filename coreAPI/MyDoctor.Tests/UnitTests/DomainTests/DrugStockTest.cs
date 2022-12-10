using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class DrugStockTest
    {
        private const string NAME = "nume";
        private const string DESCRIPTION = "desc";
        private const double PRICE = 50;
        private const uint QUANTITY = 10;

        [Fact]
        public void Create()
        {
            // When
            var ds = new DrugStock();

            // Then
            Assert.NotEqual(Guid.Empty, ds.Id);

            Assert.Empty(ds.Drugs);
            Assert.Null(ds.MedicalRoom);
            Assert.Equal(Guid.Empty, ds.MedicalRoomId);
        }

        [Fact]
        public void AttachToMedicalRoom()
        {
            // Given
            var ds = new DrugStock();
            var mr = new MedicalRoom("");

            // When
            ds.AttachToMedicalRoom(mr);

            // Then
            Assert.Equal(mr.Id, ds.MedicalRoomId);
            Assert.True(ReferenceEquals(mr, ds.MedicalRoom));
        }

        [Fact]
        public void RegisterDrugsToDrugStock_EmptyParameter()
        {
            // Given
            var ds = new DrugStock();

            // When
            var actual = ds.RegisterDrugsToDrugStock(new List<Drug>());

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void RegisterDrugsToDrugStock_Valid()
        {
            // Given
            var ds = new DrugStock();

            // When
            var actual = ds.RegisterDrugsToDrugStock(new List<Drug>()
            {
                new Drug("d1", "d1", 10, 10),
                new Drug("d2", "d2", 10, 10)
            });

            // Then
            Assert.True(actual.IsSuccess);
        }
    }
}
