
namespace Store.API.Dto
{
    public class ImageDTO
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
