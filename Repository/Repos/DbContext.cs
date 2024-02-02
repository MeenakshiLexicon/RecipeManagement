using Microsoft.Data.SqlClient;
using WebApplication_P_2_Inlamning.Repository.Interfaces;

namespace WebApplication_P_2_Inlamning.Repository.Repos
{
    public class DbContext :IDBContext
    {
        private readonly string? _connString;
        public DbContext(IConfiguration config)
        {
            _connString = config.GetConnectionString("DBConnection");

        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connString);

        }

        
    }
}
