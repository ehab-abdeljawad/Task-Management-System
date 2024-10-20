using System.ComponentModel.DataAnnotations;

namespace Task_Management_System.DTOs
{
    public class loginDtos
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
