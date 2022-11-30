namespace MyDoctor.API.DTOs
{
    public class DisplayDrugStockDto
    {
        public DisplayDrugStockDto(Guid id, Guid medicalRoomId)
        {
            this.Id = id;
            this.MedicalRoomId = medicalRoomId;
        }
        public Guid Id { get; set; }
        public Guid MedicalRoomId { get; set; }
    }
}
