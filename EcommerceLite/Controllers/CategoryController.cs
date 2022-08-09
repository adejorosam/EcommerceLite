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

            return Ok(category);
        }

    }
}