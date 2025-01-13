
namespace Store.DataAccess.Postgress.Models
{
    public class ImagesEntity
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
