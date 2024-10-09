using ProjetoReferencia.Domain.DTO.Bike.Request;
using ProjetoReferencia.Domain.DTO.Bike.Response;

namespace ProjetoReferencia.Domain.Interfaces.Services
{
    public interface IBikeService
    {
        Task<BikeResponseDto> GetByIdAsync(int id);
        Task<BikeResponseDto> RegisterBikeAsync(BikeRequestDto bike);
        Task<IEnumerable<BikeResponseDto>> GetBikesAsync(string? plate);
        Task<IEnumerable<BikeResponseDto>> GetAllAsync();
        Task<BikeResponseDto> UpdateBikeAsync(int id, string placa);
        Task<string> DeleteAsync(int id);
    }
}
