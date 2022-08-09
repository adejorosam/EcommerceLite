﻿namespace EcommerceLite.Models.Domain
{
    public class Cart_Product : BaseEntity
    {
        public Guid CartId { get; set; }

        public Cart Cart { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

    }
}