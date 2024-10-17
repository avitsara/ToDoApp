using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Server.Data;
using ToDoApp.Server.Models;
using BCrypt.Net;
using ToDoApp.Server.DTO;

namespace ToDoApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            user.password_hash = BCrypt.Net.BCrypt.HashPassword(user.password_hash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User registered successfully" });
        }*/

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new User
            {
                user_name = registerDTO.user_name,
                email = registerDTO.email,
                password_hash = BCrypt.Net.BCrypt.HashPassword(registerDTO.password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "User registered successfully" });
        }

    }
}
