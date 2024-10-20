using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_Management_System.Model
{
    public class TaskS
    {
        [Key]
        public  int id { get; set; }
        public string Titel { get; set; }
        public string  Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        [ForeignKey(nameof(User))]
        public string UserID {  get; set; }

        public ApplicationUser User { get; set; }


    }
}
