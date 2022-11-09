namespace MyDoctor.Bussiness.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Doctor Doctor { get; set; }

        public List<Drug> Drugs { get; set; }
    }
}
