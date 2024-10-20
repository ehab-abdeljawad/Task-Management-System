using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task_Management_System.Validation;

namespace Task_Management_System.DTOs
{
    public class TaskDTO
    {
        public int id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Titel { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        [FutureDate(ErrorMessage = "Due date cannot be in the past.")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } 
        public string UserID { get; set; }

        public TaskDTO()
        {
            IsCompleted = false; 
            
        }
    }
}
