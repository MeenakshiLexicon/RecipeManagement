using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;
using System.Text;
using WebApplication_P_2_Inlamning.Models.Entities;
using WebApplication_P_2_Inlamning.Repository.Interfaces;
using WebApplication_P_2_Inlamning.Repository.Repos;

namespace WebApplication_P_2_Inlamning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepo _recipeRepo;
        private readonly ICategoryRepo _categoryRepo;
        public RecipeController(IRecipeRepo repo, ICategoryRepo categoryRepo)
        {
            _recipeRepo = repo;
            _categoryRepo = categoryRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var recipe = _recipeRepo.GetAllRecipe();
                // Return HTTP 200 OK with the recipe data
                return Ok(recipe);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{title}")]
        public IActionResult GetWithTitle(string title)
        {
            try
            {
                var recipes = _recipeRepo.GetRecipeTitle(title);
                // Return HTTP 200 OK with the recipe data
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred to show recipe With Title: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("Category")]
        public IActionResult GetAllCategory()
        {
            var category = _categoryRepo.GetAllCategory();
            //Returnerar kod 200
            return Ok(category);
        }

    }
}
