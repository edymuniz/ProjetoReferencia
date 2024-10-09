using ProjetoReferencia.Domain.Entity;

namespace ProjetoReferencia.Domain.Interfaces.Repositories
{
    public interface IBikeRepository
    {
        Task<Bike> GetByIdAsync(int id);
        Task<bool> IsPlateUniqueAsync(string plate);
        Task<Bike> AddAsync(Bike bike);
        Task<IEnumerable<Bike>> GetBikesByPlateAsync(string? plate);
        Task<IEnumerable<Bike>> GetAllBikesAsync();
        Task<Bike> UpdatePlateAsync(int id, string newPlate);
        Task<string> DeleteAsync(int id);
    }
}
