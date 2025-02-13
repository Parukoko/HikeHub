using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HikeHub.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class UserController : Controller
    {
        public static List<User> users = new List<User>
        {
            new User
            {
                UserID = 1,
                UserName = "Valentine",
                Password = "123",
                Name = "Punnawit Sukhumvada",
                Sex = "Male",
                Birthdate = new DateTime(2005, 2, 14)
            },
            new User
            {
                UserID = 2,
                UserName = "Candy",
                Password = "123",
                Name = "Pusita Chuasiriporn",
                Sex = "Female",
                Birthdate = new DateTime(2005, 12, 10)
            },
            new User
            {
                UserID = 3,
                UserName = "Carrot",
                Password = "123",
                Name = "Pusida Chuasiriporn",
                Sex = "Female",
                Birthdate = new DateTime(2005, 12, 10)
            }
        };
        private static Dictionary<int, List<int>> followers = new Dictionary<int, List<int>>();

        [HttpPost("register/{username}/{password}/{name}/{sex}/{birthdate}")]
        public IActionResult Register(string username, string password, string name, string sex, DateTime birthdate)
        {
            if (users.Any(u => u.UserName == username))
            {
                return BadRequest("Username already exists.");
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sex) || birthdate == default)
            {
                return BadRequest("All fields are required.");
            }

            if (sex != "Male" && sex != "Female")
            {
                return BadRequest("Sex must be either 'Male' or 'Female'.");
            }

            // Generate UserID
            var user = new User
            {
                UserID = users.Count > 0 ? users.Max(u => u.UserID) + 1 : 1,
                UserName = username,
                Password = password,
                Name = name,
                Sex = sex,
                Birthdate = birthdate
            };

            users.Add(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("login/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Simulate login by returning a success message
            return Ok("User logged in successfully.");
        }

        [HttpPost("logout/{userID}")]
        public IActionResult Logout(int userID)
        {
            // Simulate logout by returning a success message
            return Ok("User logged out successfully.");
        }

        [HttpPut("updateProfile/{userID}")]
        public IActionResult UpdateProfile(int userID, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.UserID == userID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Name = updatedUser.Name;
            user.Birthdate = updatedUser.Birthdate;
            user.Bio = updatedUser.Bio;
            user.Sex = updatedUser.Sex;
            user.ProfilePicture = updatedUser.ProfilePicture;

            return Ok("User profile updated successfully.");
        }

        [HttpPost("followUser/{userID}/{followUserID}")]
        public IActionResult FollowUser(int userID, int followUserID)
        {
            var user = users.FirstOrDefault(u => u.UserID == userID);
            var followUser = users.FirstOrDefault(u => u.UserID == followUserID);

            if (user == null || followUser == null)
            {
                return NotFound("User not found.");
            }

            if (user.FollowList.Contains(followUserID))
            {
                return BadRequest("Already following this user.");
            }

            user.FollowList.Add(followUserID);
            followUser.FollowerList.Add(userID);

            return Ok("User followed successfully.");
        }
    }
}