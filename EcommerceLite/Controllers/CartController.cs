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

            //int quantity = addToCartRequest.Quantity;
            //check if cartId is in the request
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
                cart = new Cart()
                {

                    Quantity = quantity,
                    Price = addToCartRequest.Price,
                    TotalPrice = totalPrice,
                    //UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id")),
                    UserId = Guid.Parse("cf183488-7e41-443b-9aaf-cb4a9958a15e")
                };

                await ecommerceLiteDbContext.AddAsync(cart);
                await ecommerceLiteDbContext.SaveChangesAsync();


                var cartProduct = new Cart_Product()
                {
                    ProductId = addToCartRequest.ProductId,
                    CartId = cart.Id
                };

                await ecommerceLiteDbContext.AddAsync(cartProduct);
                await ecommerceLiteDbContext.SaveChangesAsync();
                

            }
            //else
            //{
            //     var cartProduct = await cartRepository.GetCartProductAsync(addToCartRequest.CartId, addToCartRequest.ProductId);
            //     cart = await cartRepository.GetAsync(addToCartRequest.CartId);
            //     if(cart == null)
            //     {
            //        ModelState.AddModelError(nameof(addToCartRequest.CartId),
            //            $"{nameof(addToCartRequest.CartId)} is invalid");
            //    }
            //    else if(cartProduct != null)
            //    {
            //        int quantity = 0;
            //        double totalPrice = 0.0;

            //        if (string.IsNullOrEmpty(addToCartRequest.Quantity.ToString()))
            //        {
            //             quantity = cart.Quantity + 1;
            //             totalPrice = quantity * product.SellingPrice;
            //        }
            //        else
            //        {
            //            quantity = addToCartRequest.Quantity;
            //            totalPrice = quantity * product.SellingPrice;
            //        }


            //        cart.Quantity = quantity;
            //        cart.TotalPrice = totalPrice;
            //        await ecommerceLiteDbContext.SaveChangesAsync();

            //    }
            //}
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