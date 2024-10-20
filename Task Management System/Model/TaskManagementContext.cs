using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Task_Management_System.Model
{
    public class TaskManagementContext : IdentityDbContext<ApplicationUser>
    {

        public TaskManagementContext(DbContextOptions<TaskManagementContext> options) : base(options)
        {
        }

        public DbSet<TaskS> TaskS { get; set; }

    }
}
