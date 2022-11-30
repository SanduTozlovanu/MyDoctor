using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Drug
    {
        public Drug(string name, string description, double price, uint quantity) 
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Quantity = quantity;
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
            this.DrugStockId = drugStock.Id;
            this.DrugStock = drugStock;
        }

        public Result GetDrugs(uint quantity)
        {
            if ((this.Quantity - quantity) < 0 )       
            {
                return Result.Failure("You cannot consume more drugs that the stock has.");
            }
            this.Quantity -= quantity;
            return Result.Success();
        }

    }
}
