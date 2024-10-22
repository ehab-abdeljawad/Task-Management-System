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
        
        public string Password { get; set; }
        [Required]

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
