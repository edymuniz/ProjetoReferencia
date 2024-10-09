namespace ProjetoReferencia.Domain.DTO.DeliveryDrivers.Request
{
    public class DeliveryDriverRequestDto
    {
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime Birthday { get; set; }
        public string NumberCNH { get; set; }
        public string TypeCNH { get; set; }
        public string ImageCNH { get; set; }
    }
}