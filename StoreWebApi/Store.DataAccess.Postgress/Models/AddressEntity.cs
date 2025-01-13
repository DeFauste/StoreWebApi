
namespace Store.DataAccess.Postgress.Models
{
    public class AddressEntity
    {
        public Guid Id { get; set; }
        public string Counrty { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
    }
}
