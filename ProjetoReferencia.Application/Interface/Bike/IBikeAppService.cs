using ProjetoReferencia.Domain.DTO.Bike.Request;
using ProjetoReferencia.Domain.DTO.Bike.Response;

namespace ProjetoReferencia.Application.Interface.Bike
{
    public interface IBikeAppService
    {
        Task<BikeResponseDto> PostAsync(BikeRequestDto request);
        Task<BikeResponseDto> PutAsync(int id, string placa);
        
        Task<BikeResponseDto> GetByIdAsync(int id);
        Task<string> DeleteAsync(int id);
        Task<IEnumerable<BikeResponseDto>> GetBikesAsync(string? plate);
        Task<IEnumerable<BikeResponseDto>> GetAllAsync();


    }
}
