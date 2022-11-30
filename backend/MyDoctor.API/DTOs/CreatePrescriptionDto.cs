using MyDoctor.API.Dtos;

namespace MyDoctor.API.DTOs
{
    public class CreatePrescriptionDto
    {
        public string Description { get; set; }
        public string Name { get; set; }

        public List<GetDrugDto> drugs { get; set; }
    }
}
