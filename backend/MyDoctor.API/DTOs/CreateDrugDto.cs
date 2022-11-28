namespace MyDoctor.API.Dtos
{
    public class CreateDrugDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}
