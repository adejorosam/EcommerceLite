using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceLite.Models.DTO
{
    public class AddToCartRequest
    {
        public Guid? CartId { get; set; }

        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Guid ProductId { get; set; }

    }
}