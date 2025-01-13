
namespace Store.DataAccess.Postgress.Models
{
    public class SupplierEntiry
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid AddressId { get; set; }
        public AddressEntity? Address { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
