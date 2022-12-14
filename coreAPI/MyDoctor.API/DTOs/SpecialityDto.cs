namespace MyDoctor.API.DTOs
{
    public class CreateSpecialityDto
    {
        public CreateSpecialityDto(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
    public class DisplaySpecialityDto
    {
        public DisplaySpecialityDto(Guid id, string name)
        {
            this.Id = id;
            Name = name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
