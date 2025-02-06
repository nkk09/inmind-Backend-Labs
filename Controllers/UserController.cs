using Microsoft.AspNetCore.Mvc;
using lab1_nour_kassem.Models;
using lab1_nour_kassem.Services;

namespace lab1_nour_kassem.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        
       
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                return Ok(_userService.GetUserById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<List<User>> GetUsersByName(string name)
        {
            try
            {
                return Ok(_userService.GetUsersByName(name));
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { errror = e.Message });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
        }


        [HttpPost("update")]
        public IActionResult ChangeUserName([FromBody] UserUpdateRequest request)
        {
            try
            {
                _userService.ChangeUserName(request);
                return Ok(new { message = "User updated successfully" });
            }
            catch (ArgumentNullException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }

        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok(new { message = "User deleted successfully" });
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { error = e.Message });
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new { error = e.Message });
            }
        }
    }
}