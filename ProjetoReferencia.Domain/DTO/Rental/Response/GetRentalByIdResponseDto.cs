namespace ProjetoReferencia.Domain.DTO.Rental.Response
{
    public class GetRentalByIdResponseDto
    {
        public string Identifier { get; set; }
        public int DayValue { get; set; }
        public string DeliveryDriverId { get; set; }
        public string BikeId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime DatePreviewEnd { get; set; }
        public DateTime DateReturn { get; set; }

    }
}