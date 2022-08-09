using System.ComponentModel.DataAnnotations;

namespace EcommerceLite.Models.Domain
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
