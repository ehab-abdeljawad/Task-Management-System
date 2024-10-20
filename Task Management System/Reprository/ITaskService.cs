using Task_Management_System.DTOs;

namespace Task_Management_System.Reprository
{
    public interface ITaskService
    {
        public List<TaskDTO> GetTasks(string userid);
        public TaskDTO GetTask_byID(string userid,int id);

        public void AddTask(TaskDTO task);

        public void UpdateTask(TaskDTO task);

        public void DeleteTask(int id);
        public bool it_is_found(int id);



    }
}
