using System.ComponentModel.DataAnnotations;

namespace Final_Project.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string user_Lastname { get; set; } = string.Empty;
        [Required]
        public string user_Firstname { get; set; } = string.Empty;
        [Required, DataType(DataType.EmailAddress), EmailAddress]
        public string user_Email { get; set; } = string.Empty;
        [Required]
        public string user_PhoneNumber { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string user_Password { get; set; } = string.Empty;
        [Required, Compare(nameof(user_Password)), DataType(DataType.Password)]
        public string user_ConfirmPassword { get; set; } = string.Empty;
    }
}
