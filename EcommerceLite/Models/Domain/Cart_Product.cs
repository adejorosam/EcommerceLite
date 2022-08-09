using System;
namespace EcommerceLite.Models.Domain
{
    public class Cart_Product : BaseEntity
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public Cart Cart { get; set; }
    }
}

