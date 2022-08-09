using Microsoft.AspNetCore.Mvc;
using EcommerceLite.Models.Domain;
using AutoMapper;
using EcommerceLite.Models.Repositories;

namespace EcommerceLite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var category = await categoryRepository.GetAllAsync();

            //return Ok(category);
            return Ok(new { message = "Data successfully retrieved", data = category });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] Models.DTO.AddCategoryRequest addCategoryRequest)
        {

            // map model to new user object
            var category =  mapper.Map<Category>(addCategoryRequest);

            category = await categoryRepository.AddAsync(category);


            return Ok(category);


            //return CreatedAtAction(nameof(GetCategoryAsync), new { id = walkDTO.Id }, walkDTO);

        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetCategoryAsync")]
        public async Task<IActionResult> GetCategoryAsync(Guid id)
        {
            var category = await categoryRepository.GetAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            //Get category from DB
            var category = await categoryRepository.DeleteAsync(id);

            //If null, not found
            if (category == null)
            {
                return NotFound();
            }

            //return Ok response
            return Ok(category);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateCategoryRequest updateCategoryRequest)
        {
            // Convert DTO to Domain model
            var category = mapper.Map<Category>(updateCategoryRequest);


            //Update Walk difficulty using repository
            category = await categoryRepository.UpdateAsync(id, category);

            // If null, NOT FOUND
            if (category == null)
            {
                return NotFound();
            }


            //Return Ok response
            return Ok(category);

        }

    }
}