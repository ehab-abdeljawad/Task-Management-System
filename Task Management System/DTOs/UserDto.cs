using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.DTOs
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
