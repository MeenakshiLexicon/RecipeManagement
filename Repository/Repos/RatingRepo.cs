using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;
using WebApplication_P_2_Inlamning.Models.Entities;
using WebApplication_P_2_Inlamning.Repository.Interfaces;

namespace WebApplication_P_2_Inlamning.Repository.Repos
{
    public class RatingRepo :IRatingRepo
    {
        private readonly IDBContext _context;
        public RatingRepo(IDBContext context)
        {
                _context = context;

        }

        public string InsertRating(Ratings rating, string userId)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserId", userId);
                    parameters.Add("@RecipeId", rating.RecipeId);
                    parameters.Add("@Rating", rating.Rating);
                    // Assuming the stored procedure returns the @ResultCode as an output parameter
                    parameters.Add("@ResultCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    // Execute the stored procedure
                    db.Execute("AddRating", parameters, commandType: CommandType.StoredProcedure);

                    // Retrieve the result code from the output parameter
                    string resultCode = parameters.Get<string>("@ResultCode");

                    return resultCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in AddRating: {ex.Message}");
                throw ex;

            }

        }
    }
}
