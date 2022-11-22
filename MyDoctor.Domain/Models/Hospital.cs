namespace MyDoctor.Domain.Models
{
    public class Hospital
    {
        public Hospital(string name, string adress) 
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Adress = adress;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Adress { get; private set; }
    }
}
