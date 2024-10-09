using System.ComponentModel.DataAnnotations;

namespace ProjetoReferencia.Domain.DTO.Bike.Request
{
    public class BikeRequestDto
    {
        [Required]
        public string Identifier { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]{3}\d{4}$", ErrorMessage = "Placa deve estar no formato AAA0000.")]
        public string Plate { get; set; }
    }
}
