using Store.DataAccess.Postgress.Models;
using System.ComponentModel.DataAnnotations;

namespace Store.API.Dto
{
    public class SupplierDTO
    {
        public Guid Id { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid AddressId { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
