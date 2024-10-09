using Microsoft.EntityFrameworkCore;
using ProjetoReferencia.Domain.Entity;

namespace ProjetoReferencia.Infra.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bike> Bike { get; set; }
        public DbSet<DeliveryDriver> DeliveryDrivers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<BikeImage> BikeImages { get; set; }
        public DbSet<DeliveryDriverImage> DeliveryDriverImages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Bike>(entity =>
            {
                entity.HasKey(e => e.Id); 

            });

            
        }
    }
}
