namespace MyDoctor.API.DTOs
{
    public class CreateMedicalRoomDto
    {
        public CreateMedicalRoomDto(string adress)
        {
            Adress = adress;
        }

        public string Adress { get; set; }
    }

    public class DisplayMedicalRoomDto
    {
        public DisplayMedicalRoomDto( Guid id, string adress) 
        { 
            this.Id = id;
            this.Adress = adress;
        }
        public Guid Id { get; set; }
        public string Adress { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DisplayMedicalRoomDto dto &&
                   Id.Equals(dto.Id) &&
                   Adress == dto.Adress;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Adress);
        }
    }
}
