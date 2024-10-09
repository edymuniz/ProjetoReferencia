using Microsoft.AspNetCore.Http;
using ProjetoReferencia.Domain.DTO.DeliveryDrivers.Request;

namespace ProjetoReferencia.Application.Interface.DeliveryDrivers
{
    public interface IDeliveryDrivesAppService
    {
        Task<string> PostAsync(DeliveryDriverRequestDto request);

        Task<string> SaveFileAsync(IFormFile file);

    }
}
