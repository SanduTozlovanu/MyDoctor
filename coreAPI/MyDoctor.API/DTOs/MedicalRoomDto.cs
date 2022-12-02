namespace MyDoctor.API.DTOs
{
    public class CreateMedicalRoomDto
    {
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
    }
}
