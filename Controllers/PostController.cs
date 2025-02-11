using Microsoft.AspNetCore.Mvc;
using HikeHub.Models;
using Microsoft.EntityFrameworkCore;
using HikeHub.ViewModels;

namespace HikeHub.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly HikeHubDbContext _context;

        public PostsController(HikeHubDbContext context) // ✅ Constructor name fixed
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ViewPost), new { id = post.PostID }, post);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditPost(int id, [FromBody] Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            post.Title = updatedPost.Title;
            post.MeetingLocation = updatedPost.MeetingLocation;
            post.DestinationAddress = updatedPost.DestinationAddress;
            post.Duration = updatedPost.Duration;
            post.Description = updatedPost.Description;
            post.MaxParticipants = updatedPost.MaxParticipants;
            post.Date = updatedPost.Date;
            post.Time = updatedPost.Time;
            post.ExpiredAt = updatedPost.ExpiredAt;
            post.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(post);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("view/{id}")]
        public async Task<IActionResult> ViewPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .FirstOrDefaultAsync(p => p.PostID == id);

            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost("toggle-favorite/{postId}")]
        public async Task<IActionResult> ToggleFavorite(int postId, [FromBody] int userId)
        {
            var favorite = await _context.Favorites
                .SingleOrDefaultAsync(f => f.UserID == userId && f.PostID == postId);

            if (favorite == null)
                _context.Favorites.Add(new Favorite { UserID = userId, PostID = postId });
            else
                _context.Favorites.Remove(favorite);

            await _context.SaveChangesAsync();
            return Ok(new { message = favorite == null ? "Added to favorites" : "Removed from favorites", postId });
        }

        [HttpPost("join-trip/{postId}")]
        public async Task<IActionResult> JoinTrip(int postId, [FromBody] int userId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
                return NotFound();

            if (post.MaxParticipants <= 0)
                return BadRequest(new { message = "No more spots available.", postId });

            var existingParticipant = await _context.Participants
                .FirstOrDefaultAsync(p => p.UserID == userId && p.PostID == postId);

            if (existingParticipant != null)
                return BadRequest(new { message = "User already joined this trip.", postId });

            _context.Participants.Add(new Participant { UserID = userId, PostID = postId });
            post.MaxParticipants--;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Successfully joined trip", postId });
        }
    }
}
