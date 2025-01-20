
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store.DataAccess.Postgress.Models
{
    public class ClientEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public string ClientSurname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.UtcNow;
        public bool Gender { get; set; } = true;
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public Guid AddressId { get; set; }
        public AddressEntity? Address { get; set; }
    }
}
