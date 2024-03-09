using System.ComponentModel.DataAnnotations;

namespace Final_Project.DTOs
{
   
    public class LoginDTO
    {
        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string user_Email { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string user_Password { get; set; } = string.Empty;
    }
}

