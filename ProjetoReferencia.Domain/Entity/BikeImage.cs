namespace ProjetoReferencia.Domain.Entity
{
    public class BikeImage
    {
        public int Id { get; set; }
        public int BikeId { get; set; }
        public string ImageUrl { get; set; }

        public Bike Bike { get; set; }
    }
}
