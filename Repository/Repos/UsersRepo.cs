using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using WebApplication_P_2_Inlamning.Models.Entities;
using WebApplication_P_2_Inlamning.Repository.Interfaces;

namespace WebApplication_P_2_Inlamning.Repository.Repos
{
    public class UsersRepo : IUserRepo
    {

        private readonly IDBContext _context;

        public UsersRepo(IDBContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByUsernameAndPassword(string userid, string password)
        {
            // Implement the logic to fetch the user from the database based on the username and password

            using (var db = _context.GetConnection())
            {
                var user = await db.QueryFirstOrDefaultAsync<Users>("SELECT * FROM Users WHERE UserId = @UserId AND Password = @Password",
                    new { UserId = userid, Password = password });

                return user;
            }
        }


        public List<Users> GetAllUser()
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    return db.Query<Users>("ShowAllUsers", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAllUsers: {ex.Message}");
                throw;
            }
        }

        public void Insert(Users user)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserId", user.UserId);
                    parameters.Add("@Name", user.Name);
                    parameters.Add("@Password", user.Password);
                    parameters.Add("@Email", user.Email);
                    parameters.Add("@UserType", user.UserType);


                    db.Execute("AddNewUser", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertUser: {ex.Message}");

            }
        }

        public void Update(string userId, Users user)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserId", user.UserId);
                    parameters.Add("@Name", user.Name);
                    parameters.Add("@Password", user.Password);
                    parameters.Add("@Email", user.Email);
                    parameters.Add("@UserType", user.UserType);


                    db.Execute("UpdateUsers", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertUser: {ex.Message}");

            }
        }

        public void Delete(string userId)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@UserId", userId);

                    db.Execute("DeleteUser", parameters, commandType: CommandType.StoredProcedure);
                
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Delete: {ex.Message}");

            }
        }
    }


    
}
