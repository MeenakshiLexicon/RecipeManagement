using System.Text.Json.Serialization;

namespace WebApplication_P_2_Inlamning.Models.Entities
{
    public class Users
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string UserType { get; set; }

        public Users(string userId, string name, string password, string email, string userType)
        {
            UserId = userId;
            Name = name;
            Password = password;
            Email = email;
            UserType = userType;
        }
        public Users()
        {
            UserId = "defaultUserId";
            UserType = "User";
        }





    }
}
