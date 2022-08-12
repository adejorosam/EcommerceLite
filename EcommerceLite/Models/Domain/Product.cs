using System.ComponentModel.DataAnnotations;
namespace EcommerceLite.Models.Domain
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float CostPrice { get; set; }

        public float SellingPrice { get; set; }

        public Guid Sku { get; set; }

        public int AvailableQuantity { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid CategoryId { get; set; }

        public Guid UserId { get; set; }




        // Navigation property
        public User User { get; set; }

        public Category Category { get; set; }

        public List<Cart_Product> CartProducts { get; set; }

    }
}

