using System.ComponentModel.DataAnnotations;
namespace EcommerceLite.Models.Domain
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public float CostPrice { get; set; }

        [Required]
        public float SellingPrice { get; set; }

        public Guid Sku { get; set; }

        [Required]
        public int AvailableQuantity { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }




        // Navigation property
        public User User { get; set; }

        public Category Category { get; set; }

        public List<Cart_Product> CartProducts { get; set; }

    }
}

