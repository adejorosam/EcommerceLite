using EcommerceLite.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EcommerceLite.Models.Domain;

namespace EcommerceLite.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public CartController(ICartRepository cartRepository, IProductRepository productRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCartAsync([FromBody] Models.DTO.AddToCartRequest addToCartRequest)
        {
            // Validate the request
            if (!(await ValidateAddToCartAsync(addToCartRequest)))
            {
                return BadRequest(ModelState);
            }

            // map model to new user object
            //var cart = mapper.Map<Cart>(addToCartRequest); // Convert DTO to Domain model
            var cart = new Models.Domain.Cart()
            {
                Quantity = addToCartRequest.Quantity,
                Price = addToCartRequest.Price,
                //UserId = 
                //Produ = addToCartRequest.RegionId,
                //WalkDifficultyId = addToCartRequest.WalkDifficultyId
            };

            //check if cart exists
            if (string.IsNullOrEmpty(addToCartRequest.CartId.ToString()))
            {
                cart = await cartRepository.AddAsync(cart);

            }



            return Ok(cart);


            //return CreatedAtAction(nameof(GetProductAsync), new { id = walkDTO.Id }, walkDTO);

        }

        #region
        private async Task<bool> ValidateAddToCartAsync(Models.DTO.AddToCartRequest addToCartRequest)
        {
            var product = await productRepository.GetAsync(addToCartRequest.ProductId);
            if (product == null)
            {
                ModelState.AddModelError(nameof(addToCartRequest.ProductId),
                        $"{nameof(addToCartRequest.ProductId)} is invalid");
            }

            if(product.AvailableQuantity < 1)
            {
                ModelState.AddModelError($"{product.Name} is out of stock");
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