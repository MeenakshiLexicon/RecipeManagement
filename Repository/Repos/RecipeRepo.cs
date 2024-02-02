using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication_P_2_Inlamning.Models.Entities;
using WebApplication_P_2_Inlamning.Repository.Interfaces;


namespace WebApplication_P_2_Inlamning.Repository.Repos
{
    public class RecipeRepo : IRecipeRepo
    {
        private readonly IDBContext _context;
        public RecipeRepo(IDBContext context) 
        { 
            _context = context;
        
        }
        public List<Recipes> GetAllRecipe()
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    return db.Query<Recipes>("GetRecipe", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAllRecipe: {ex.Message}");
                throw;
            }

        }
        public List<Recipes> GetRecipeTitle(string title)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    // Pass the title as a parameter to the stored procedure
                    var parameters = new { Title = title };
                    return db.Query<Recipes>("GetRecipeByTitle", parameters, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetRecipeTitle: {ex.Message}");
                throw;
            }
        }

        public void Insert(Recipes recipe)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserId", recipe.UserId);
                    parameters.Add("@Title", recipe.Title);
                    parameters.Add("@Ingredients", recipe.Ingredients);
                    parameters.Add("@Description", recipe.Description);
                    parameters.Add("@CategoryName", recipe.CategoryName);


                    db.Execute("AddRecipe", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in InsertAd: {ex.Message}");

            }

        }

        public string Delete(int recipeId, string userId)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@RecipeId", recipeId);
                    parameters.Add("@UserId", userId);
                    parameters.Add("@ResultCode", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

                    db.Execute("DeleteRecipe", parameters, commandType: CommandType.StoredProcedure);
                    string resultCode = parameters.Get<string>("@ResultCode");

                    return resultCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in Delete: {ex.Message}");
                throw ex;
            }
        }

        public void Update(Recipes updateRecipe)
        {
            try
            {
                using (IDbConnection db = _context.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@RecipeId",  updateRecipe.RecipeId);
                    parameters.Add("@UserId", updateRecipe.UserId);
                    parameters.Add("@Title", updateRecipe.Title);
                    parameters.Add("@Description", updateRecipe.Description);
                    parameters.Add("@Ingredients", updateRecipe.Ingredients);
                    parameters.Add("@CategoryName", updateRecipe.CategoryName);
                    db.Execute("UpdateRecipe", parameters, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in UpdateRecipe: {ex.Message}");
            }
        }

       
    }
}
