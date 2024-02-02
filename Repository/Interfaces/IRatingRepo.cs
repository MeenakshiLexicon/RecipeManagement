using WebApplication_P_2_Inlamning.Models.Entities;

namespace WebApplication_P_2_Inlamning.Repository.Interfaces
{
    public interface IRatingRepo
    {
        string InsertRating(Ratings rating, string userId);
    }
}
