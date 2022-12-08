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

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                DisplayDrugDto dto = (DisplayDrugDto)obj;
                return (this.Id == dto.Id) && (this.Name == dto.Name) && (this.Price == dto.Price) && (this.Description == dto.Description) && (this.Quantity == dto.Quantity);
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.DrugStockId, this.Name, this.Description, this.Price, this.Quantity);
        }
    }
}
