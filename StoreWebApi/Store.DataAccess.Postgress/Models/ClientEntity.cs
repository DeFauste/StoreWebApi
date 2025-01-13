
namespace Store.DataAccess.Postgress.Models
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientSurname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public bool Gender { get; set; } = true;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public Guid AddressId { get; set; }
        public AddressEntity? Address { get; set; }
    }
}
