namespace MyDoctor.API.Dtos
{
    public class GetDrugDto
    {
        public Guid drugId { get; set; }
        public uint Quantity { get; set; }
    }
}
