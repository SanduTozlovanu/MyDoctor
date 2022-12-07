using MyDoctor.Domain.Models;
using MyDoctor.Tests.Orderers;
using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class DrugTest
    {
        private const string NAME = "nume";
        private const string DESCRIPTION = "desc";
        private const double PRICE = 50;
        private const uint QUANTITY = 10;

        [Fact]
        public void Create()
        {
            // When
            var d = new Drug(NAME, DESCRIPTION, PRICE, QUANTITY);

            // Then
            Assert.NotEqual(Guid.Empty, d.Id);
            Assert.Equal(NAME, d.Name);
            Assert.Equal(DESCRIPTION, d.Description);
            Assert.Equal(PRICE, d.Price);
            Assert.Equal(QUANTITY, d.Quantity);

            Assert.Null(d.DrugStock);
            Assert.Equal(Guid.Empty, d.DrugStockId);
        }

        [Fact]
        public void AttachToDrugStock()
        {
            // Given
            var d = new Drug(NAME, DESCRIPTION, PRICE, QUANTITY);
            var ds = new DrugStock();

            // When
            d.AttachToDrugStock(ds);

            // Then
            Assert.Equal(ds.Id, d.DrugStockId);
            Assert.True(ReferenceEquals(ds, d.DrugStock));
        }

        [Fact]
        public void GetDrugs_Invalid()
        {
            // Given
            var d = new Drug(NAME, DESCRIPTION, PRICE, QUANTITY);

            // When
            var actual = d.GetDrugs(QUANTITY + 1);

            // Then
            Assert.True(actual.IsFailure);
        }

        [Fact]
        public void GetDrugs_Valid()
        {
            // Given
            var d = new Drug(NAME, DESCRIPTION, PRICE, QUANTITY);

            // When
            var actual = d.GetDrugs(0);

            // Then
            Assert.True(actual.IsSuccess);
        }
    }
}
