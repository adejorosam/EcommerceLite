using System.ComponentModel.DataAnnotations;
namespace EcommerceLite.Models.Domain
{
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool isActive { get; set; }
    }
}

