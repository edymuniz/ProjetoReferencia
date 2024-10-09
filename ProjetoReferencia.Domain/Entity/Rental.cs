namespace ProjetoReferencia.Domain.Entity
{
    public class Rental
    {
        public int Id { get; set; }
        public int BikeId { get; set; }
        public int DeliveryDriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal DailyRate { get; set; }
        public decimal? Fine { get; set; }
        public Bike Bike { get; set; }
        public DeliveryDriver DeliveryDriver { get; set; }
    }
}
