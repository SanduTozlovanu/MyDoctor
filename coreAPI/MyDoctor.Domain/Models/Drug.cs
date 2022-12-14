using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Drug
    {
        public Drug(DrugStock drugStock, string name, string description, double price, uint quantity)
        {
            Id = Guid.NewGuid();
            drugStock.RegisterDrugsToDrugStock(new List<Drug> { this });
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
        public Guid Id { get; private set; }
        public virtual DrugStock DrugStock { get; private set; }
        public Guid DrugStockId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public uint Quantity { get; private set; }

        public void AttachToDrugStock(DrugStock drugStock)
        {
            DrugStockId = drugStock.Id;
            DrugStock = drugStock;
        }

        public Result GetDrugs(uint quantity)
        {
            if (Quantity < quantity)
            {
                return Result.Failure("You cannot consume more drugs that the stock has.");
            }
            Quantity -= quantity;
            return Result.Success();
        }

    }
}
