using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using lab1_nour_kassem.Models;

namespace lab1_nour_kassem.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new()
        {
            new User { Id = 1, Name = "Nour", Email = "nour@example.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@example.com" },
            new User { Id = 3, Name = "Jill", Email = "jill@example.com" },
            new User { Id = 4, Name = "Sam", Email = "sam@example.com" }
        };
        
        //i am checking for errors and sending error messages but I didn't throw exceptions "literally"
        //I assumed it was enough because I'm running out of time, but for later, what exactly does the question mean?

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (id<=0) return BadRequest(new { error = "Invalid Id" });
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound(new { error = "User not found" });
            return Ok(user);
        }

        [HttpGet("name/{name}")]
        public ActionResult<List<User>> GetUsersByName(string name)
        {
            var users = _users.Where(u => u.Name.Contains(name));
            //I tried to make it case insensitive but it didnt work...
            //var users = _users.Where(u => u.Name.toLower().Contains(name.toLower()));
            
            if (users.Count() == 0) return NotFound(new { error = "No user matches the query" });
            return Ok(_users.Where(u => u.Name.Contains(name)));
        }


        [HttpPost("update")]
        public IActionResult ChangeUserName([FromBody] UserUpdateRequest request)
        {
            var user = _users.FirstOrDefault(u => u.Id == request.Id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            
            //hon it raises a bad request error because of the user.cs input validation requirements
            //so i decided not to check again
            user.Name = request.NewName;
            user.Email = request.Email;

            return Ok(new { message = "User name updated successfully.", user });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            if (id<=0) return BadRequest(new { error = "Invalid Id" });
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            _users.Remove(user);
            return Ok(new { message = "User deleted successfully.", user });
            
        }
    }
}