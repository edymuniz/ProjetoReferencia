namespace ProjetoReferencia.Domain.Entity
{
    public class Payment
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Rental Rental { get; set; }
    }
}
