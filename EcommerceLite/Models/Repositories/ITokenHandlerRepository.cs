using System;
using EcommerceLite.Models.Domain;

namespace EcommerceLite.Models.Repositories
{
    public interface ITokenHandlerRepository
    {
        Task<string> CreateTokenAsync(User user);

    }
}

