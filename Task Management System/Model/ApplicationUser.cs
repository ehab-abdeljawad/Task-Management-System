using Microsoft.AspNetCore.Identity;

namespace Task_Management_System.Model
{
    public class ApplicationUser : IdentityUser
    {



        virtual public List<TaskS> Tasks { get; set; }

    }
}
