using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace HikeHub.Controllers
{
    [Route("api/alluser")]
    [ApiController]
    public class AllUserController : Controller
    {
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            return Ok(UserController.users);
        }
    }
}