using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HikeHub.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class UserController : Controller
    {
        private static List<User> users = new List<User>();
        private static Dictionary<int, List<int>> followers = new Dictionary<int, List<int>>();

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (users.Any(u => u.UserName == user.UserName))
            {
                return BadRequest("Username already exists.");
            }

            users.Add(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = users.FirstOrDefault(u => u.UserName == loginUser.UserName && u.Name == loginUser.Name);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Simulate login by returning a success message
            return Ok("User logged in successfully.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Simulate logout by returning a success message
            return Ok("User logged out successfully.");
        }

        [HttpPut("updateProfile")]
        public IActionResult UpdateProfile([FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.UserID == updatedUser.UserID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Name = updatedUser.Name;
            user.Age = updatedUser.Age;
            user.Bio = updatedUser.Bio;
            user.Gender = updatedUser.Gender;
            user.ProfilePicture = updatedUser.ProfilePicture;

            return Ok("User profile updated successfully.");
        }

        [HttpPost("followUser")]
        public IActionResult FollowUser(int userId, int followUserId)
        {
            if (!users.Any(u => u.UserID == userId) || !users.Any(u => u.UserID == followUserId))
            {
                return NotFound("User not found.");
            }

            if (!followers.ContainsKey(userId))
            {
                followers[userId] = new List<int>();
            }

            if (followers[userId].Contains(followUserId))
            {
                return BadRequest("Already following this user.");
            }

            followers[userId].Add(followUserId);
            return Ok("User followed successfully.");
        }
    }
}