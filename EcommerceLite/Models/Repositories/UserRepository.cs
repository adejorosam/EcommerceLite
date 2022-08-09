using EcommerceLite.Data;
using EcommerceLite.Models.DTO;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;



namespace EcommerceLite.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceLiteDbContext ecommerceLiteDbContext;


        public UserRepository(EcommerceLiteDbContext ecommerceLiteDbContext)
        {
            this.ecommerceLiteDbContext = ecommerceLiteDbContext;
        }

        

        public async Task<Domain.User> GetEmailAsync(string emailAddress)
        {
            var user = await ecommerceLiteDbContext.Users.FirstOrDefaultAsync(x => x.EmailAddress.ToLower() == emailAddress);

            return user;
        }

        public async Task<Domain.User> AuthenticateAsync(string emailAddress, string password)
        {
            var user = ecommerceLiteDbContext.Users.SingleOrDefault(x => x.EmailAddress == emailAddress);

            // validate
            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }
                //throw new AppException("Username or password is incorrect");

            // authentication successful
            //var response = mapper.Map<AuthenticateResponse>(user);
            //response.Token = _jwtUtils.GenerateToken(user);
            return user;

        }


        public async Task<Domain.User> RegisterAsync(Domain.User user)
        {
            user.Id = Guid.NewGuid();
            await ecommerceLiteDbContext.Users.AddAsync(user);
            await ecommerceLiteDbContext.SaveChangesAsync();
            return user;
            
        }

        
    }
}
