using System.Text.Json.Serialization;

namespace WebApplication_P_2_Inlamning.Models.Entities
{
    public class Ratings
    {
        [JsonIgnore]
        public int RatingId { get; set; }
       

        public int RecipeId { get; set; }

        public int Rating { get; set; }

        public Ratings(int ratingId,  int recipeId, int rating)
        {
            RatingId = ratingId;
           
            RecipeId = recipeId;
            Rating = rating;
        }
        public Ratings()
        {
            
        }
    }
}
