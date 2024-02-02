using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication_P_2_Inlamning.Models.Entities;
using WebApplication_P_2_Inlamning.Repository.Interfaces;

namespace WebApplication_P_2_Inlamning.Repository.Repos
{
    public class CategoriesRepo : ICategoryRepo
    {
        private readonly IDBContext _context;
        public CategoriesRepo(IDBContext context)
        {
            _context = context;

        }
      public  List<Categories> GetAllCategory()
      {
            try
            {
                using (IDbConnection dbConnection = _context.GetConnection())
                {
                    return dbConnection.Query<Categories>("GetAllCategory", commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                // Log the exception details (replace Console.WriteLine with your logging mechanism)
                Console.WriteLine($"An error occurred in GetAllCategory: {ex.Message}");
                throw;
            }

      }
       
    }

}
