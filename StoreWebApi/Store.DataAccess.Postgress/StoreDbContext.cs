using Microsoft.EntityFrameworkCore;
using Store.DataAccess.Postgress.Configurations;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        public DbSet<AddressEntity> Address { get; set; }
        public DbSet<ClientEntity> Client { get; set; }
        public DbSet<ImagesEntity> Images { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<SupplierEntiry> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ImageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
