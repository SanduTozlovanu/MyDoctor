using MyDoctorApp.Domain.Models;

namespace MyDoctor.Tests.UnitTests.DomainTests
{
    public class SpecialityTest
    {
        private const string SPECIALITY_NAME = "Chirurg";
        public static Speciality CreateDefaultSpeciality()
        {
            return new Speciality(SPECIALITY_NAME);
        }

        [Fact]
        public void Create()
        {
            // When
            var sp = CreateDefaultSpeciality();

            // Then
            Assert.NotEqual(Guid.Empty, sp.Id);

            Assert.Empty(sp.Doctors);
            Assert.Equal(SPECIALITY_NAME, sp.Name);
        }

        [Fact]
        public void RegisterDoctorTest()
        {
            // Given
            var sp = CreateDefaultSpeciality();
            var doc = DoctorTest.CreateDefaultDoctor();

            // When
            sp.RegisterDoctor(doc);

            // Then
            Assert.True(ReferenceEquals(doc, sp.Doctors[0]));
            Assert.True(ReferenceEquals(sp, doc.Speciality));
            Assert.Equal(sp.Id, doc.SpecialityID);

        }
    }
}
