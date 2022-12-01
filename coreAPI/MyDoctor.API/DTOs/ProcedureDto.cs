namespace MyDoctor.API.DTOs
{
    public class CreateProcedureDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
    public class DisplayProcedureDto
    {
        public DisplayProcedureDto(Guid id, Guid prescriptionId, string description, double price, string name) 
        { 
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Description = description; 
            this.PrescriptionId= prescriptionId;  
        }
        public Guid Id { get; set; }
        public Guid PrescriptionId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
    }
}
