using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication_P_2_Inlamning.Models.Entities;
using WebApplication_P_2_Inlamning.Repository.Interfaces;

namespace WebApplication_P_2_Inlamning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepo _userRepo;
        public AdminController(IHttpContextAccessor httpContextAccessor, IUserRepo repo)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepo = repo;
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            try
            {
                var userTypeClaim = User.FindFirst(ClaimTypes.Role)?.Value;
                if (userTypeClaim == "Admin")
                {
                    var users = _userRepo.GetAllUser();
                    // Return HTTP 200 with users
                    return Ok(users);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAll: {ex.Message}");

                // Return HTTP 500 Internal Server Error with an error message
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("addUser")]
        public IActionResult Insert(Users user)
        {
            try
            {
                var userTypeClaim = User.FindFirst(ClaimTypes.Role)?.Value;
                if(userTypeClaim == "Admin")
                {
                    _userRepo.Insert(user);
                    // If successful, return status code 201
                    return StatusCode(201);
                }
                else
                {
                    return Unauthorized();
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpDelete("{userId}")]
        public IActionResult Delete(string userId)
        {
            try
            {
                var userTypeClaim = User.FindFirst(ClaimTypes.Role)?.Value;
                if (userTypeClaim == "Admin")
                {
                    
                    _userRepo.Delete(userId);

                    // If successful, return HTTP 200 OK
                    return Ok("User Delete Successfully");
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");

                // Return an appropriate error response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPut("{userId}")]
        public IActionResult Update(string userId, Users user)
        {

            try
            {
                var userTypeClaim = User.FindFirst(ClaimTypes.Role)?.Value;
                if (userTypeClaim == "Admin")
                {
                    user.UserId = userId;
                    _userRepo.Update(userId, user);

                    // If successful, return a 204 No Content response
                    return NoContent();
                }
                else
                {
                    return Unauthorized();
                }
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
