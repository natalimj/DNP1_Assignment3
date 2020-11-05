using System.ComponentModel.DataAnnotations;

namespace Family_Web_API.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserType{ get; set; }
      
        
    }
}