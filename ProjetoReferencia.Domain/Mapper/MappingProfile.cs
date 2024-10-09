using AutoMapper;
using ProjetoReferencia.Domain.DTO.Bike.Request;
using ProjetoReferencia.Domain.DTO.Bike.Response;
using ProjetoReferencia.Domain.Entity;

namespace ProjetoReferencia.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapear de Bike para BikeResponseDto
            CreateMap<Bike, BikeResponseDto>();

            // Mapear de BikeRequestDto para Bike
            CreateMap<BikeRequestDto, Bike>();
        }
    }
}
