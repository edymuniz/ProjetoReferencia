using Microsoft.AspNetCore.Http;
using ProjetoReferencia.Domain.DTO.DeliveryDrivers.Request;
using ProjetoReferencia.Domain.ExternalServices.AWS;

namespace ProjetoReferencia.Application.Interface.DeliveryDrivers
{
    public class DeliveryDrivesAppService : IDeliveryDrivesAppService
    {
        private readonly IStorageService _storageService;

        public DeliveryDrivesAppService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public Task<string> PostAsync(DeliveryDriverRequestDto request)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
           return await _storageService.SaveFileAsync(file);
        }
    }
}