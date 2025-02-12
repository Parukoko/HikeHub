using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HikeHub.Controllers
{
    public class UserController : Controller
    {
        private static List<User> users = new List<User>();
        private static Dictionary<int, List<int>> followers = new Dictionary<int, List<int>>();

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists.");
                return View(user);
            }

            users.Add(user);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User loginUser)
        {
            var user = users.FirstOrDefault(u => u.UserName == loginUser.UserName && u.Name == loginUser.Name);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginUser);
            }

            // Simulate login by setting a session or cookie
            // HttpContext.Session.SetString("UserID", user.UserID.ToString());

            return RedirectToAction("Profile", new { id = user.UserID });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Simulate logout by clearing the session or cookie
            // HttpContext.Session.Remove("UserID");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult UpdateProfile(int id)
        {
            var user = users.FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateProfile(User updatedUser)
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

            return RedirectToAction("Profile", new { id = user.UserID });
        }

        [HttpPost]
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

        [HttpGet]
        public IActionResult Profile(int id)
        {
            var user = users.FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return View(user);
        }
    }
}