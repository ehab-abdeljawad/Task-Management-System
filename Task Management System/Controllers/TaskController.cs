using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task_Management_System.DTOs;
using Task_Management_System.Reprository;

namespace Task_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController (ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet]
        [Authorize]
        public IActionResult Get(string userid)
        {
        List<TaskDTO> tasks = _taskService.GetTasks(userid);

            if(tasks.Count > 0 ) { 
            
            return Ok(tasks);
            
            }

            return NoContent();

        }
        [HttpGet("task")]
        [Authorize]
        public IActionResult task(string userid , int taskID)
        {
            if(taskID != null && userid != null)
            {

                TaskDTO taskDTO = _taskService.GetTask_byID(userid, taskID);
                if(taskDTO != null)
                {
                    return Ok(taskDTO);
                }
                else
                {
                    return NoContent();
                }
            }

            return BadRequest();

        }


        // adding new task
        [HttpPost]
        [Authorize]
        public ActionResult addtask(TaskDTO taskDTO)
        {
            if(ModelState.IsValid)
            {
                taskDTO.IsCompleted = false;
                _taskService.AddTask(taskDTO);

                return Ok();
                
            }

            return BadRequest(ModelState);
        }
        
        //Updating task
        [HttpPut]
        [Authorize]
        public ActionResult updatetask(TaskDTO taskDTO)
        {

           
            if(ModelState.IsValid )
            {
                _taskService.UpdateTask(taskDTO); return Ok();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Authorize]
        public ActionResult deletetask(int id) { 

             if(id == 0 ||! _taskService.it_is_found(id))
            { return NoContent(); }
             _taskService.DeleteTask(id);
              return Ok();
        
        
        }
    }
}
