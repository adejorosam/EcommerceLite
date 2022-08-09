using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceLite.Models.Domain
{
    public class Cart : BaseEntity
    {
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public double TotalPrice { get; set; }




        // Navigation properties
        public User User { get; set; }


        public List<Cart_Product> CartProducts { get; set; }

    }
}

