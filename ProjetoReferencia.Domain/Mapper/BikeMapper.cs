using ProjetoReferencia.Domain.DTO.Bike.Response;
using ProjetoReferencia.Domain.Entity;

namespace ProjetoReferencia.Domain.Mapper
{
    public static class BikeMapper
    {
        public static BikeResponseDto ToBikeResponseDto(this Bike bike)
        {
            return new BikeResponseDto
            {
                Identifier = bike.Identifier,
                Plate = bike.Plate,
                Model = bike.Model,
                Year = bike.Year
            };
        }
    }
}
