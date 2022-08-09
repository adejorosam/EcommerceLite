using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceLite.Models.DTO
{
    public class AddCategoryRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

    }
}

