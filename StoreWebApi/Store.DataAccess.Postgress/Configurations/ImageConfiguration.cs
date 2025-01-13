using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.DataAccess.Postgress.Models;

namespace Store.DataAccess.Postgress.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<ImagesEntity>
    {
        public void Configure(EntityTypeBuilder<ImagesEntity> builder)
        {
            builder.
                HasKey(i => i.Id);
        }
    }
}
