using System.ComponentModel.DataAnnotations;

namespace Store.API.Dto
{
    public class AdressDTO
    {
        public Guid Id { get; set; }
        [Required, MaxLength(30)]
        public string Country { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        public string City { get; set; } = string.Empty;
        [Required, MaxLength(30)]
        public string Street { get; set; } = string.Empty;
    }
}
