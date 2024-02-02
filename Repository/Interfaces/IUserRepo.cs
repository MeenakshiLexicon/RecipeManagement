using WebApplication_P_2_Inlamning.Models.Entities;

namespace WebApplication_P_2_Inlamning.Repository.Interfaces
{
    public interface IUserRepo
    {
       
       List<Users> GetAllUser();
       void Insert(Users user);
       void Update(string userId, Users user);
       void Delete(string userId);
       Task<Users> GetUserByUsernameAndPassword(string username, string password);

    }
}

