using Microsoft.AspNetCore.Mvc;
using HikeHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace HikeHub.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly HikeHubDbContext _context;
        public static List<User> Users = new List<User>
        {
            new User
            {
                UserID = 1,
                UserName = "valentine",
                Password = "password1",
                FirstName = "Punnawit",
                LastName = "Sukhumvada",
                Birthdate = new DateTime(2005, 2, 14),
                Sex = "Male",
                Tel = 123456789,
                IdLine = "valentine123",
                Bio = "I love hiking",
                Image = "profile1.jpg"
            },
            new User
            {
                UserID = 2,
                UserName = "carrot",
                Password = "password2",
                FirstName = "Pusida",
                LastName = "Chuasiriporn",
                Birthdate = new DateTime(2005, 12, 10),
                Sex = "Female",
                Tel = 987654321,
                IdLine = "carrot123",
                Bio = "I love traveling",
                Image = "profile2.jpg"
            },
            new User
            {
                UserID = 3,
                UserName = "candy",
                Password = "password3",
                FirstName = "Pusita",
                LastName = "Chuasiriporn",
                Birthdate = new DateTime(2005, 12, 10),
                Sex = "Female",
                Tel = 123123123,
                IdLine = "candy123",
                Bio = "I love cooking",
                Image = "profile3.jpg"
            }
        };

        public UserController(HikeHubDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Posts)
                .Include(u => u.Favourites)
                .Include(u => u.Participants)
                .FirstOrDefaultAsync(u => u.UserID == id);

            if (user == null)
                return NotFound();

            var userDto = new UserDto
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Sex = user.Sex,
                Tel = user.Tel,
                IdLine = user.IdLine,
                Bio = user.Bio,
                Image = user.Image,
                FollowedList = user.FollowList.Select(followId => new FollowInfo
                {
                    UserID = followId,
                    UserName = Users.FirstOrDefault(u => u.UserID == followId)?.UserName ?? string.Empty
                }).ToList(),
                FollowerList = user.FollowerList.Select(followerId => new FollowInfo
                {
                    UserID = followerId,
                    UserName = Users.FirstOrDefault(u => u.UserID == followerId)?.UserName ?? string.Empty
                }).ToList()
            };

            return Ok(userDto);
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            var userDtos = Users.Select(user => new UserDto
            {
                UserID = user.UserID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Sex = user.Sex,
                Tel = user.Tel,
                IdLine = user.IdLine,
                Bio = user.Bio,
                Image = user.Image,
                FollowedList = user.FollowList.Select(followId => new FollowInfo
                {
                    UserID = followId,
                    UserName = Users.FirstOrDefault(u => u.UserID == followId)?.UserName ?? string.Empty
                }).ToList(),
                FollowerList = user.FollowerList.Select(followerId => new FollowInfo
                {
                    UserID = followerId,
                    UserName = Users.FirstOrDefault(u => u.UserID == followerId)?.UserName ?? string.Empty
                }).ToList()
            }).ToList();

            return Ok(userDtos);
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password, string firstname, string lastname, string sex, DateTime birthdate)
        {
            if (Users.Any(u => u.UserName == username))
            {
                return BadRequest("Username already exists.");
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(sex) || birthdate == default)
            {
                return BadRequest("All fields are required.");
            }

            // Validate username
            username = username.ToLower();
            if (!Regex.IsMatch(username, @"^[a-z0-9]+$"))
            {
                return BadRequest("Username must contain only lowercase letters and numbers.");
            }

            // Validate firstname and lastname
            if (!Regex.IsMatch(firstname, @"^[a-zA-Z]+$") || !Regex.IsMatch(lastname, @"^[a-zA-Z]+$"))
            {
                return BadRequest("Firstname and lastname must contain only letters.");
            }

            sex = sex.ToLower() switch
            {
                "male" => "Male",
                "female" => "Female",
                "m" => "Male",
                "f" => "Female",
                _ => sex
            };

            if (sex != "Male" && sex != "Female")
            {
                return BadRequest("Sex must be either 'Male' or 'Female'.");
            }

            firstname = char.ToUpper(firstname[0]) + firstname.Substring(1).ToLower();
            lastname = char.ToUpper(lastname[0]) + lastname.Substring(1).ToLower();

            int newUserId = Users.Count > 0 ? Users.Max(u => u.UserID) + 1 : 1;

            var newUser = new User
            {
                UserID = newUserId,
                UserName = username,
                Password = password,
                FirstName = firstname,
                LastName = lastname,
                Birthdate = birthdate,
                Sex = sex,
                Tel = null,
                IdLine = null,
                Bio = null,
                Image = null
            };

            Users.Add(newUser);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var user = Users.FirstOrDefault(u => u.UserName == loginUser.UserName && u.Password == loginUser.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("User logged in successfully.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("User logged out successfully.");
        }

        [HttpPut("updateProfile")]
        public IActionResult UpdateProfile([FromBody] User updatedUser)
        {
            var user = Users.FirstOrDefault(u => u.UserID == updatedUser.UserID);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Birthdate = updatedUser.Birthdate;
            user.Bio = updatedUser.Bio;
            user.Sex = updatedUser.Sex;
            user.ProfilePicture = updatedUser.ProfilePicture;

            return Ok("User profile updated successfully.");
        }

        [HttpPost("followUser")]
        public IActionResult FollowUser(int userId, int followUserId)
        {
            var user = Users.FirstOrDefault(u => u.UserID == userId);
            var followUser = Users.FirstOrDefault(u => u.UserID == followUserId);

            if (user == null || followUser == null)
            {
                return NotFound("User not found.");
            }

            if (user.FollowList.Contains(followUserId))
            {
                return BadRequest("Already following this user.");
            }

            user.FollowList.Add(followUserId);
            followUser.FollowerList.Add(userId);

            return Ok("User followed successfully.");
        }
    }
}

