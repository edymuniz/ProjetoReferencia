using ProjetoReferencia.Domain.DTO.Bike.Request;
using ProjetoReferencia.Domain.DTO.Bike.Response;
using ProjetoReferencia.Domain.Entity;
using ProjetoReferencia.Domain.Interfaces.Services;

namespace ProjetoReferencia.Application.Interface.Bike
{
    public class BikeAppService : IBikeAppService
    {
        private readonly IBikeService _bikeService;

        public BikeAppService(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        public async Task<string> DeleteAsync(int id)
        {
            return await _bikeService.DeleteAsync(id);
        }

        public async Task<IEnumerable<BikeResponseDto>> GetAllAsync()
        {
            return await _bikeService.GetAllAsync();
        }

        public async Task<BikeResponseDto> GetByIdAsync(int id)
        {
            return await _bikeService.GetByIdAsync(id);
            
        }

        public async Task<BikeResponseDto> PostAsync(BikeRequestDto bike)
        {
            return await _bikeService.RegisterBikeAsync(bike);
        }

        public async Task<BikeResponseDto> PutAsync(int id, string placa)
        {
            return await _bikeService.UpdateBikeAsync(id, placa);
        }

        public async Task<IEnumerable<BikeResponseDto>> GetBikesAsync(string? plate)
        {

            if (!string.IsNullOrEmpty(plate))
            {
                return await _bikeService.GetBikesAsync(plate);
            }

            return null;
        }
    }
}
