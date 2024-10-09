using Microsoft.EntityFrameworkCore;
using ProjetoReferencia.Domain.Entity;
using ProjetoReferencia.Domain.Interfaces.Repositories;
using ProjetoReferencia.Infra.Data;

namespace ProjetoReferencia.Infra.Repository
{
    public class BikeRepository : IBikeRepository
    {
        private readonly ApplicationDbContext _context;

        public BikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Bike> GetByIdAsync(int id)
        {
            return await _context.Bike.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Bike> AddAsync(Bike bike)
        {
            try
            {
                await _context.Bike.AddAsync(bike);
                await _context.SaveChangesAsync();
                return bike;
            }catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<bool> IsPlateUniqueAsync(string plate)
        {
            return !await _context.Bike.AnyAsync(b => b.Plate == plate);
        }

        public async Task<IEnumerable<Bike>> GetAllBikesAsync()
        {
            return await _context.Bike.ToListAsync();
        }

        public async Task<IEnumerable<Bike>> GetBikesByPlateAsync(string plate)
        {
            return await _context.Bike
                .Where(bike => bike.Plate.Contains(plate))
                .ToListAsync();
        }

        public async Task<Bike> UpdatePlateAsync(int id, string newPlate)
        {

            var bike = await _context.Bike.FirstOrDefaultAsync(b => b.Id == id);
            if (bike == null)
            {
                return null;
            }

            bike.Plate = newPlate;

            try
            {
                _context.Bike.Update(bike);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            return bike; 
        }

        public async Task<string> DeleteAsync(int id)
        {

            var bike = await _context.Bike.FirstOrDefaultAsync(b => b.Id == id);
            if (bike == null)
            {
                return "Moto não encontrada.";
            }

            try
            {
                _context.Bike.Remove(bike);
                await _context.SaveChangesAsync();
                return "Moto removida com sucesso.";
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro
                return $"Erro ao remover moto: {ex.Message}";
            }
        }
    }
}