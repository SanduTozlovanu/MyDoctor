namespace MyDoctor.API.DTOs
{
    public class DisplayBillDto
    {
        public DisplayBillDto(Guid id, Guid AppointmentId, double billPrice)
        {
            this.Id = id;
            this.AppointmentId = AppointmentId;
            this.BillPrice = billPrice;
        }
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public double BillPrice { get; set; }
    }
}
