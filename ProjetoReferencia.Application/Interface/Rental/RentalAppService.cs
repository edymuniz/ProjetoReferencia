using ProjetoReferencia.Domain.DTO.Rental.Request;
using ProjetoReferencia.Domain.DTO.Rental.Response;

namespace ProjetoReferencia.Application.Interface.Rental
{
    public class RentalAppService : IRentalAppService
    {
        public Task<GetRentalByIdResponseDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> PostAsync(RentalRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<string> PutAsync(int id, RentalDateReturnRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
