using System.Text.Json.Serialization;

namespace WebApplication_P_2_Inlamning.Models.Entities
{
    public class Recipes
    {
        //[JsonIgnore]
        public int RecipeId { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
       // public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double Rating { get; }



        public Recipes(int recipeId, string userId, string title, string ingredients, string description,string categoryName)
        {
            RecipeId = recipeId;
            UserId = userId;
            Title = title;
            Ingredients = ingredients;
            Description = description;
            CategoryName = categoryName;
            //CategoryId = categoryId;
        }
        public Recipes() 
        {
            UserId = "defaultUserId";
        }

    }
}
