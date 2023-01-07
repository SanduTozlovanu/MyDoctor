namespace MyDoctor.Application.Responses
{
    public class MedicalRoomResponse : BaseResponse
    {
        public MedicalRoomResponse(Guid id, string adress)
        {
            this.Id = id;
            this.Adress = adress;
        }
        public Guid Id { get; set; }
        public string Adress { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is MedicalRoomResponse response &&
                   Id.Equals(response.Id) &&
                   Adress == response.Adress;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Adress);
        }
    }
}
