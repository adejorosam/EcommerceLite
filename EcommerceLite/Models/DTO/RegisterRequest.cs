using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceLite.Models.DTO
{
    public class RegisterRequest
    {

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

