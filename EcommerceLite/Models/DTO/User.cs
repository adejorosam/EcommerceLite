using System;
using System.Text.Json.Serialization;

namespace EcommerceLite.Models.DTO
{
    public class User
    {
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

    }
}

