namespace WebApplication_P_2_Inlamning.Models.Entities
{
    public class Categories
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public Categories(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}
