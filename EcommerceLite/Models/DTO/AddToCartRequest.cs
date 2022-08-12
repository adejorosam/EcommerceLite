using System;
namespace EcommerceLite.Models.DTO
{
    public class AddToCartRequest
    {
        public Guid CartId { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double TotalPrice { get; set; }

        public Guid ProductId { get; set; }

        public Guid UserId { get; set; }

    }
}