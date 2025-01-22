using System.ComponentModel.DataAnnotations;

namespace Store.API.Dto
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        [Required, MaxLength(30)]
        public string ClientName { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        public string ClientSurname { get; set; } = string.Empty;
        [Required]
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; } = true;
        public Guid AddressId { get; set; }
    }
}
