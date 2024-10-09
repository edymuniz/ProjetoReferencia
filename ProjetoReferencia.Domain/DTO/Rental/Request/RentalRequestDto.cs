namespace ProjetoReferencia.Domain.DTO.Rental.Request
{
    public class RentalRequestDto
    {
        public string DeliveryDriverId { get; set; }
        public string BikeId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DatePreviewEnd { get; set; }
        public int PlaneId { get; set; }
    }
}
