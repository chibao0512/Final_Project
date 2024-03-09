using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Final_Project.Models
{
    public class ApplicationUser
    {
        [Key]
        public int user_Id { get; set; }

        public string user_Lastname { get; set; } = string.Empty;
        public string user_Firstname { get; set; } = string.Empty;
        public string user_Email { get; set; } = string.Empty;
        public string user_Password { get; set; } = string.Empty;
        public string user_PhoneNumber { get; set; } = string.Empty;
    }
}
