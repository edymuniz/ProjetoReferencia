using System.ComponentModel.DataAnnotations;

namespace ProjetoReferencia.Domain.Entity
{
    public class Bike
    {
        [Key]
        public int Id { get; set; }
        public string Identifier { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}