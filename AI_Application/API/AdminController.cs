using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AI_Application.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        // In-memory user store (demo purpose only)
        private static readonly List<NewUser> Users = new();

        [HttpPost("create-user")]
        public IActionResult CreateUser([FromBody] NewUser user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Users.Any(u => u.Username == user.Username))
                return Conflict(new { Message = "Username already exists" });

            Users.Add(user);

            return Ok(new
            {
                Message = "User created successfully",
                user.Username,
                user.Role
            });
        }

        [HttpGet("list-users")]
        public IActionResult ListUsers()
        {
            return Ok(Users.Select(u => new
            {
                u.Username,
                u.Role
            }));
        }

        [HttpGet("get-user/{username}")]
        public IActionResult GetUser(string username)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(new
            {
                user.Username,
                user.Role
            });
        }

        [HttpPut("update-user/{username}")]
        public IActionResult UpdateU(string username, [FromBody] UpdateUser updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;

            return Ok(new { Message = "User updated successfully" });
        }

        [HttpDelete("delete-user/{username}")]
        public IActionResult DeleteUser(string username)
        {
            var user = Users.FirstOrDefault(u => u.Username == username);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            Users.Remove(user);
            return Ok(new { Message = "User deleted successfully" });
        }

        public class NewUser
        {
            [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            public string Username { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

            [Required]
            [MinLength(6)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

            [Required]
            [RegularExpression("Student|Faculty|Admin", ErrorMessage = "Role must be Student, Faculty, or Admin")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            public string Role { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        }

        public class UpdateUser
        {
            [Required]
            [MinLength(6)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

            [Required]
            [RegularExpression("Student|Faculty|Admin", ErrorMessage = "Role must be Student, Faculty, or Admin")]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
            public string Role { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        }
    }
}
