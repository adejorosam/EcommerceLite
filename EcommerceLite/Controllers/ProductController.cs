using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EcommerceLite.Models.Repositories;
using EcommerceLite.Models.Domain;

namespace EcommerceLite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await productRepository.GetAllAsync();

            return Ok(new { message = "Data successfully retrieved", data = products });
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            //Get Product from DB
            var product = await productRepository.DeleteAsync(id);

            //If null, not found
            if (product == null)
            {
                return NotFound();
            }

            //return Ok response
            return Ok(product);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetProductAsync")]
        public async Task<IActionResult> GetProductAsync(Guid id)
        {
            var product = await productRepository.GetAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        [Route("categories/{categoryId:guid}")]
        [ActionName("GetProductAsync")]
        public async Task<IActionResult> GetProductsByCategoryAsync(Guid categoryId)
        {
            var product = await productRepository.GetProductsByCategoryAsync(categoryId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] Models.DTO.AddProductRequest addProductRequest)
        {

            // map model to new user object
            var product = mapper.Map<Product>(addProductRequest);

            product = await productRepository.AddAsync(product);


            return Ok(product);


            //return CreatedAtAction(nameof(GetProductAsync), new { id = walkDTO.Id }, walkDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateProductRequest updateProductRequest)
        {
            // Convert DTO to Domain model
            var product = mapper.Map<Product>(updateProductRequest);


            //Update product using repository
            product = await productRepository.UpdateAsync(id, product);

            // If null, NOT FOUND
            if (product == null)
            {
                return NotFound();
            }


            //Return Ok response
            return Ok(product);

        }
    }
}