using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntiry>
    {
        public void Configure(EntityTypeBuilder<SupplierEntiry> builder)
        {
            builder.
                HasKey(s => s.Id);

            builder.
                HasOne(s => s.Address);
        }
    }
}
