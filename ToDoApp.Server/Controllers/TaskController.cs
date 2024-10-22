using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Server.Data;
using ToDoApp.Server.DTO;
using ToDoApp.Server.Models;
using Task = ToDoApp.Server.Models.Task;


namespace ToDoApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserTasks(int userId)
        {
            if (userId <= 0) 
            {
                return BadRequest(new { Message = "Invalid user ID" });
            }

            var tasks = await _context.Tasks.Where(t => t.user_id == userId).ToListAsync();
            if (!tasks.Any()) 
            {
                return NotFound(new { Message = "No tasks found for this user" });
            }

            return Ok(tasks);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateTask([FromBody] TaskDTO taskDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userExists = await _context.Users.AnyAsync(u => u.user_id == taskDTO.user_id);
            if (!userExists) return BadRequest(new { Message = "User not found" });

            var task = new Task
            {
                user_id = taskDTO.user_id,
                title = taskDTO.title,
                description = taskDTO.description,
                is_completed = taskDTO.is_completed,
                created_at = DateTime.Now, 
                updated_at = DateTime.Now   
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Task created successfully", Task = task });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Task deleted successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTaskCompletion(int id, [FromBody] TaskDTO taskDTO)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            task.is_completed = taskDTO.is_completed;
            task.updated_at = DateTime.Now; 

            await _context.SaveChangesAsync();
            return Ok(new { Message = "Task updated successfully", Task = task });
        }


    }

}

