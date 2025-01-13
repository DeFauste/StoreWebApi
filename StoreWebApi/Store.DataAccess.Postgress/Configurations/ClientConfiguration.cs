using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
    {
        public void Configure(EntityTypeBuilder<ClientEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.
                HasOne(c => c.Address);

        }
    }
}
