using Microsoft.AspNetCore.Http;

namespace ProjetoReferencia.Domain.ExternalServices.AWS
{
    public interface IStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
    }

}
