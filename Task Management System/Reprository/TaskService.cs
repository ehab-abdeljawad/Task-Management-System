using Task_Management_System.DTOs;
using Task_Management_System.Model;

namespace Task_Management_System.Reprository
{
    public class TaskService : ITaskService
    {
        public TaskManagementContext Context { get; set; }
        public TaskService(TaskManagementContext context) { 
         
            Context = context;
        
        }
        public List<TaskDTO> GetTasks(string userid)
        {
            List<TaskS> taskS = Context.TaskS.Where(id=>id.UserID == userid).ToList();

            if (taskS.Count > 0)
            {
                List<TaskDTO> tasksDTOs = new List<TaskDTO>();
                foreach (var task in taskS)
                {
                    tasksDTOs.Add(new TaskDTO()
                    {
                        id=task.id,
                        Titel=task.Titel,
                        Description=task.Description,
                        DueDate=task.DueDate,
                        IsCompleted=task.IsCompleted,
                        UserID=task.UserID,

                    });

                    


                }
                return tasksDTOs;


            }

            return new List<TaskDTO>();
            
        }

        public TaskDTO GetTask_byID(string userid, int taskid)
        {
            TaskS task_dbcontext = Context.TaskS.Where(id=>id.UserID==userid && id.id==taskid).FirstOrDefault();
             if (task_dbcontext != null)
            {
                TaskDTO task = new TaskDTO()
                {
                    id=task_dbcontext.id,
                    Titel = task_dbcontext.Titel, Description=task_dbcontext.Description,
                    DueDate = task_dbcontext.DueDate,
                    IsCompleted = task_dbcontext.IsCompleted,
                    UserID=task_dbcontext.UserID,


                };

                return task;
            }

            return new TaskDTO();


        }

        public void AddTask(TaskDTO task)
        {

            TaskS task1 = new TaskS()
            {
                id = 0,
                Titel = task.Titel,
                Description = task.Description,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                UserID = task.UserID,
            };

            Context.TaskS.Add(task1);
            Context.SaveChanges();

            
        }

        public void UpdateTask(TaskDTO task)
        {
            
            if (task != null)
            {
                TaskS task1 = new TaskS()
                {
                    id = task.id,
                    Titel = task.Titel,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    IsCompleted = task.IsCompleted,
                    UserID = task.UserID,

                };
                Context.TaskS.Update(task1);
                Context.SaveChanges();
            }
        }

        public void DeleteTask(int id)
        {
              TaskS task = Context.TaskS.First(x => x.id == id);
            Context.TaskS.Remove(task);
            Context.SaveChanges();
        }

        public bool it_is_found(int id)
        {
            TaskS task = Context.TaskS.First(i => i.id == id);
            if(task != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
