
namespace Store.API.Dto
{
    public class IamgeDTO
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; } = Array.Empty<byte>();
    }
}
