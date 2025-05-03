using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Task_management_DataAccessLayer;

namespace Task_management_project.Controllers
{
   // [Route("api/[controller]")]
    [Route("api/Tasks")]
    [ApiController]
    public class TasksController : ControllerBase
    {

        [HttpGet("All", Name = "GetAllTasks")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<TaskDTO>> GetAllTasks()
        {
            List<TaskDTO> TasksList = Task_management_BusinessLayer.Task.GetAllTasks();

            if (TasksList.Count == 0)
            {
                return NotFound("No Tasks Found!");
            }
            return Ok(TasksList);
        }


        // POST: api/Task/register
       [HttpPost("AddTask")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TaskDTO> AddTask(TaskDTO NewTask)
        {
            if (NewTask == null || string.IsNullOrEmpty(NewTask.Name) || string.IsNullOrEmpty(NewTask.Description) || NewTask.UserId < 0)
            {
                return BadRequest("Invalid Task data.");
            }



            Task_management_BusinessLayer.Task user = new Task_management_BusinessLayer.Task(new TaskDTO(NewTask.Id, NewTask.Name, NewTask.Description, NewTask.DueDate, NewTask.Completed, NewTask.UserId));
            user.Save();

            NewTask.Id = user.Id;
            return CreatedAtRoute("GetTaskById", new { id = NewTask.Id }, NewTask);

        }


          [HttpGet("{id}", Name = "GetTaskById")]

          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          // ارجاع الطالب حسب رقمة
          public ActionResult<TaskDTO> GetTaskById(int id)
          {

              if (id < 1)
              {
                  return BadRequest($"Not accepted ID {id}");
              }
              Task_management_BusinessLayer.User user = Task_management_BusinessLayer.User.Find(id);

              if (user == null)
              {
                  return NotFound($"User with ID {id} not found.");
              }


              UserDTO UDTO = user.UDTO;


              return Ok(UDTO);

          }


        //here we use http put method for update
        [HttpPut("{id}", Name = "UpdateTask")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public ActionResult<TaskDTO> UpdateTask(int id, TaskDTO updatedTask)
          {
              if (updatedTask == null || string.IsNullOrEmpty(updatedTask.Name) || string.IsNullOrEmpty(updatedTask.Description) || updatedTask.UserId < 0)
              {
                  return BadRequest("Invalid Task data.");
              }


              //var Task = TaskDataSimulation.TasksList.FirstOrDefault(s => s.Id == id);

              Task_management_BusinessLayer.Task Task = Task_management_BusinessLayer.Task.Find(id);


              if (Task == null)
              {
                  return NotFound($"Task with ID {id} not found.");
              }


              Task.Name = updatedTask.Name;
              Task.Description = updatedTask.Description;
              Task.DueDate = updatedTask.DueDate;
              Task.Completed = updatedTask.Completed;
              Task.UserId = updatedTask.UserId;
              Task.Save();

              //we return the DTO not the full Task object.
              return Ok(Task.TDTO);

          }


          [HttpDelete("{id}", Name = "DeleteTask")]
          [ProducesResponseType(StatusCodes.Status200OK)]
          [ProducesResponseType(StatusCodes.Status400BadRequest)]
          [ProducesResponseType(StatusCodes.Status404NotFound)]
          public ActionResult DeleteTask(int id)
          {
              if (id < 1)
              {
                  return BadRequest($"Not accepted ID {id}");
              }



              if (Task_management_BusinessLayer.Task.DeleteTask(id))

                  return Ok($"Task with ID {id} has been deleted.");
              else
                  return NotFound($"Task with ID {id} not found. no rows deleted!");
          }

    }
}
