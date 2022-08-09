using BC = BCrypt.Net.BCrypt;
using EcommerceLite.Models.DTO;
using EcommerceLite.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;


namespace EcommerceLite.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly ITokenHandlerRepository tokenHandlerRepository;
        private readonly IMapper mapper;


        public AuthController(IUserRepository userRepository, ITokenHandlerRepository tokenHandlerRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.tokenHandlerRepository = tokenHandlerRepository;
            this.mapper = mapper;

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //Check email and password
            var user = await userRepository.AuthenticateAsync(loginRequest.EmailAddress, loginRequest.Password);

            if (user != null)
            {
                //Generate a JWT token
                var token = await tokenHandlerRepository.CreateTokenAsync(user);

                var userDTO = mapper.Map<User>(user);

                return Ok(new { message = "Login successful", user = userDTO, token = token });
            }

            return BadRequest("Username or Password is invalid");
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            // Validate the request
            if (!await ValidateAddUserAsync(model))
            {
                return BadRequest(ModelState);
            }

            // map model to new user object
            var user =  mapper.Map<Models.Domain.User>(model);

            // Hash password
            user.Password = BC.HashPassword(model.Password);

            // save user
            user = await userRepository.RegisterAsync(user);

            //convert back to DTO
            var userDTO = mapper.Map<User>(user);

            var token = await tokenHandlerRepository.CreateTokenAsync(user);

            return Ok(new { message = "Registration successful", user = userDTO, token = token });
        }

        #region
        private async Task<bool> ValidateAddUserAsync(RegisterRequest registerRequest)
        {
            var user = await userRepository.GetEmailAsync(registerRequest.EmailAddress);

            if (user != null)
            {
                ModelState.AddModelError(nameof(registerRequest.EmailAddress),
                        $"{nameof(registerRequest.EmailAddress)} has been taken");
            }


            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}