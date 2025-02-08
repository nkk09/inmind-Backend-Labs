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
        
        //middleware should now handle all exceptions so we remove try catch blocks from here!
       
        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        { 
            return Ok(_userService.GetUserById(id));
        }

        [HttpGet("name/{name}")]
        public ActionResult<List<User>> GetUsersByName(string name)
        {
            return Ok(_userService.GetUsersByName(name));
        }


        [HttpPost("update")]
        public IActionResult ChangeUserName([FromBody] UserUpdateRequest request)
        {
            _userService.ChangeUserName(request);
            return Ok(new { message = "User updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}