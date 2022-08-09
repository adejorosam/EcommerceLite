using System;
using EcommerceLite.Models.DTO;

namespace EcommerceLite.Models.Repositories
{
    public interface IUserRepository
    {
        Task<Models.Domain.User> AuthenticateAsync(string emailaddress, string password);

        Task<Models.Domain.User> RegisterAsync(Domain.User user);

        Task<Models.Domain.User> GetEmailAsync(string emailAddress);
    }
}
