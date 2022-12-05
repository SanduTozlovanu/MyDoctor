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

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                DisplayMedicalRoomDto dto = (DisplayMedicalRoomDto)obj;
                return (this.Id == dto.Id) && (this.Adress == dto.Adress);
            }
        }
    }
}
