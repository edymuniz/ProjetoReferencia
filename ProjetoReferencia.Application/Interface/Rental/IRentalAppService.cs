using ProjetoReferencia.Domain.DTO.Rental.Request;
using ProjetoReferencia.Domain.DTO.Rental.Response;

namespace ProjetoReferencia.Application.Interface.Rental
{
    public interface IRentalAppService
    {
        Task<string> PostAsync(RentalRequestDto request);
        Task<GetRentalByIdResponseDto> GetByIdAsync(int id);
        Task<string> PutAsync(int id, RentalDateReturnRequestDto request);

    }
}