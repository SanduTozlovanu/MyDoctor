namespace MyDoctor.API.DTOs
{
    public class CreateDrugDto
    {
        public CreateDrugDto(string name, string description, double price, uint quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public uint Quantity { get; set; }
    }
    public class GetDrugDto
    {
        public GetDrugDto(Guid drugId, uint quantity)
        {
            this.DrugId = drugId;
            Quantity = quantity;
        }

        public Guid DrugId { get; set; }
        public uint Quantity { get; set; }

    }
    public class DisplayDrugDto
    {
        public DisplayDrugDto(Guid id, Guid drugStockId, string name, string description, double price, uint quantity)
        {
            this.Id = id;
            this.Description = description;
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

        public override bool Equals(object? obj)
        {
            return obj is DisplayDrugDto dto &&
                   Id.Equals(dto.Id) &&
                   DrugStockId.Equals(dto.DrugStockId) &&
                   Name == dto.Name &&
                   Description == dto.Description &&
                   Price == dto.Price &&
                   Quantity == dto.Quantity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DrugStockId, Name, Description, Price, Quantity);
        }
    }
}
