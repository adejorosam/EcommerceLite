using EcommerceLite.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EcommerceLite.Models.Domain;
using EcommerceLite.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EcommerceLite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;
        private readonly EcommerceLiteDbContext ecommerceLiteDbContext;


        public CartController(ICartRepository cartRepository, IProductRepository productRepository, EcommerceLiteDbContext ecommerceLiteDbContext, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCartAsync")]
        public async Task<IActionResult> GetCartAsync(Guid id)
        {
            var cart = await cartRepository.GetAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCartAsync(Guid id)
        {
            //Get cart from DB
            var cart = await cartRepository.DeleteAsync(id);

            //If null, not found
            if (cart == null)
            {
                return NotFound();
            }

            //return Ok response
            return Ok(cart);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCartAsync([FromBody] Models.DTO.AddToCartRequest addToCartRequest)
        {
            // Validate the request
            if (!(await ValidateAddToCartAsync(addToCartRequest)))
            {
                return BadRequest(ModelState);
            }

            var cart = new Cart();
            var product = await productRepository.GetAsync(addToCartRequest.ProductId);

            //check if cart id was supplied
            if (string.IsNullOrEmpty(addToCartRequest.CartId.ToString()))
            {
                int quantity = 0;
                double totalPrice = 0.0;

                if (string.IsNullOrEmpty(addToCartRequest.Quantity.ToString()))
                {
                    quantity = 1;
                    totalPrice = quantity * product.SellingPrice;
                }
                else
                {
                    quantity = addToCartRequest.Quantity;
                    totalPrice = quantity * product.SellingPrice;
                }

                cart.Quantity = quantity;
                cart.Price = addToCartRequest.Price;
                cart.TotalPrice = totalPrice;
                cart.UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id"));
                //cart.UserId = Guid.Parse("cf183488-7e41-443b-9aaf-cb4a9958a15e");

                cart = await cartRepository.AddAsync(cart);

                var cartProduct = new Cart_Product()
                {
                    ProductId = addToCartRequest.ProductId,
                    CartId = cart.Id
                };

                await cartRepository.AddCartProductAsync(cartProduct);

            }
            else
            {
                var cartProduct = await cartRepository.GetCartProductAsync(addToCartRequest.CartId, addToCartRequest.ProductId);
                cart = await cartRepository.GetAsync(addToCartRequest.CartId);

                if (cart == null)
                {
                    ModelState.AddModelError(nameof(addToCartRequest.CartId),
                        $"{nameof(addToCartRequest.CartId)} is invalid");
                }
                else if (cartProduct != null)
                {
                    int quantity = 0;
                    double totalPrice = 0.0;

                    if (string.IsNullOrEmpty(addToCartRequest.Quantity.ToString()))
                    {
                        quantity = addToCartRequest.Quantity;
                        totalPrice = quantity * product.SellingPrice;
                    }
                    else
                    {
                        quantity = 1;
                        totalPrice = quantity * product.SellingPrice;
                    }


                    cart.Quantity = quantity;
                    cart.TotalPrice = totalPrice;
                    cart = await cartRepository.UpdateAsync(addToCartRequest.CartId, cart);

                }
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

            if (product.AvailableQuantity < 1)
            {
                ModelState.AddModelError(nameof(addToCartRequest.ProductId), $"{nameof(product.Name)} is out of stock");
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