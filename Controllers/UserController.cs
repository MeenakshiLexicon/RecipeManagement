using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using WebApplication_P_2_Inlamning.Repository.Interfaces;
using WebApplication_P_2_Inlamning.Repository.Repos;
using WebApplication_P_2_Inlamning.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication_P_2_Inlamning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRatingRepo _ratingRepo;
        private readonly IRecipeRepo _recipeRepo;

        public UserController(IHttpContextAccessor httpContextAccessor, IRatingRepo ratingRepo,IRecipeRepo recipeRepo)
        {
            _httpContextAccessor = httpContextAccessor;
            _ratingRepo =ratingRepo;
            _recipeRepo =recipeRepo;

        }

        [Authorize]
        [HttpPost("addRecipe")]
        public IActionResult InsertRecipe(Recipes recipe, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            try
            {
                // Skickar tillbaka kod 201
                string base64Credentials = authorizationHeader.Substring("Basic ".Length).Trim();
                string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));
                string[] parts = credentials.Split(':', 2);
                string userId = parts[0];
                recipe.UserId = userId;

                _recipeRepo.Insert(recipe);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertRecipe: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpDelete("{recipeId}")]
        public IActionResult DeleteRecipe(int recipeId, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
                string base64Credentials = authorizationHeader.Substring("Basic ".Length).Trim();
                string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));
                string[] parts = credentials.Split(':', 2);
                string userId = parts[0];

            try
            {
                
                var result = _recipeRepo.Delete(recipeId, userId);

                if (result == "Recipe deleted successfully")
                {
                    return Ok(result);
                }
                else
                    return StatusCode(400, result);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred in DeleteRecipe: {ex.Message}");

              return StatusCode(500, "Internal Server Error");
            }
        }

        [Authorize]
        [HttpPut("{recipeId}")]
        public IActionResult UpdateRecipe(int recipeId, Recipes update, [FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            string base64Credentials = authorizationHeader.Substring("Basic ".Length).Trim();
            string credentials = Encoding.UTF8.GetString(Convert.FromBase64String(base64Credentials));
            string[] parts = credentials.Split(':', 2);
            string userId = parts[0];
            update.UserId = userId;
            try
            {
                // Set the RecipeId from the route parameter
                update.RecipeId = recipeId;

                _recipeRepo.Update(update);

                // Return a 204 No Content response for successful update
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in UpdateRecipe: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }


        [HttpPost("Rating")]
        public IActionResult Insert(Ratings rating)
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);
            string userid = credentials[0];
            string password = credentials[1];
            try
            {
                // Attempt to insert the user
               var result = _ratingRepo.InsertRating(rating, userid);

                if (result == "Rating added successfully")
                {
                    return Ok(result);
                }
                else
                    return StatusCode(400, result);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }

}
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserRepo _usersRepo;

    public IUserRepo IUserRepo { get; private set; }

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserRepo usersRepo) : base(options, logger, encoder, clock)
    {
        _usersRepo = usersRepo;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
        var credentialsBytes = Convert.FromBase64String(authHeader.Parameter);
        var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);
        var username = credentials[0];
        var password = credentials[1];

        var user = await _usersRepo.GetUserByUsernameAndPassword(username, password);

        if (user == null)
            return AuthenticateResult.Fail("Invalid username or password");

        var claims = new[] { 
            new Claim(ClaimTypes.Name, user.UserId),
             new Claim(ClaimTypes.Role, user.UserType)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }




}

