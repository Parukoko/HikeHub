using Microsoft.AspNetCore.Mvc;
using HikeHub.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace HikeHub.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly HikeHubDbContext _context;

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

            return Ok(user);
        }
    }
}
