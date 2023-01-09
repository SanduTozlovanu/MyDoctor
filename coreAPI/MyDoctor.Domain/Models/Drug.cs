using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Drug
    {
        private const string TOO_MANY_CONSUMED_DRUGS_ERROR = "You cannot consume more drugs that the stock has.";

        public Drug(string name, string description, double price, uint quantity)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }
        public Guid Id { get; private set; }
        public DrugStock DrugStock { get; private set; }
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
                return Result.Failure(TOO_MANY_CONSUMED_DRUGS_ERROR);
            }
            Quantity -= quantity;
            return Result.Success();
        }

        public void Update(Drug drug)
        {
            this.Description = drug.Description;
            this.Price = drug.Price;
            this.Name = drug.Name;
            this.Quantity = drug.Quantity;
        }

    }
}