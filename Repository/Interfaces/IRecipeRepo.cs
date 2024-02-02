using WebApplication_P_2_Inlamning.Models.Entities;

namespace WebApplication_P_2_Inlamning.Repository.Interfaces
{
    public interface IRecipeRepo
    {
        List<Recipes> GetAllRecipe();
        List<Recipes> GetRecipeTitle(string Title);
        void Insert(Recipes recipe);
        void Update(Recipes recipe);
        string Delete(int recipeId, string userId);


    }
}
