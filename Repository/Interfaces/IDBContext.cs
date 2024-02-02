using Microsoft.Data.SqlClient;

namespace WebApplication_P_2_Inlamning.Repository.Interfaces
{
    public interface IDBContext
    {
        SqlConnection GetConnection();
      //  T QueryFirstOrDefault<T>(string sql, object parameters = null);

    }
}
