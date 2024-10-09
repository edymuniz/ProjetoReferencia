namespace ProjetoReferencia.Domain.Entity
{
    public class DeliveryDriverImage
    {
        public int Id { get; set; }
        public int DeliveryDriverId { get; set; }
        public string ImageUrl { get; set; }

        public DeliveryDriver DeliveryDriver { get; set; }
    }
}
