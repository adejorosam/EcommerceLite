using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceLite.Models.DTO
{
    public class AddProductRequest
    {
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

        public DateTime ExpirationDate { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}