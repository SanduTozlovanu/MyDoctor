namespace MyDoctor.API.Dtos
{
    public class CreateDrugDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
    public class GetDrugDto
    {
        public Guid drugId { get; set; }
        public uint Quantity { get; set; }

    }
    public class DisplayDrugDto
    {
        public DisplayDrugDto(Guid id, Guid drugStockId, string name, string description, double price, uint quantity)
        {
            this.Id = id;
            this.Description= description;
            this.DrugStockId = drugStockId;
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;
        }
        public Guid Id { get; set; }
        public Guid DrugStockId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
}
