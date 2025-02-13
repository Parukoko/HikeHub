using Microsoft.AspNetCore.Mvc;
using HikeHub.Models;
using HikeHub.Services;
using HikeHub.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HikeHub.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly PostService _postService;
        private readonly UserService _userService;
        private readonly TagService _tagService;
        private readonly AnnouncementService _announcementService;
        private readonly ILogger<PostController> _logger;

        public PostController(
            PostService postService,
            ILogger<PostController> logger,
            UserService userService,
            TagService tagService,
            AnnouncementService announcementService)
        {
            _postService = postService;
            _userService = userService;
            _tagService = tagService;
            _announcementService = announcementService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _postService.GetAllPostsAsync());
        }

        [HttpGet("view/{id}")]
        public async Task<IActionResult> ViewPost(int id)
        {
            var post = await _postService.GetPostByPostIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpGet("create/{id}")]
        public async Task<IActionResult> Create(int id)
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized("Invalid user ID.");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            ViewData["UserId"] = userId;
            ViewData["Username"] = user.UserName;
            ViewData["UserImage"] = user.Image;

            var tags = await _tagService.GetAllTagsAsync();

            var model = new PostViewModel
            {
                Title = "",
                Description = "",
                Tags = tags.Select(c => new TagViewModel
                {
                    TagID = c.TagID,
                    TagName = c.TagName.ToString()
                }).ToList(),
                DepartedDate = DateTime.Now,
                ReturnedDate = DateTime.Now.AddDays(5),
                ExpiredDate = DateTime.Now,
                MaxParticipants = 1,
                DestinationAddress = "Bangkok",
                MeetingLocation = "Bangkok",
                MeetingProvince = "Bangkok",
                Announcement = _announcementService.GetAnnouncementById(id).Result,
            };

            return View(model);
        }

        [HttpPost("createPost")]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            if (post == null)
                return BadRequest("Invalid post data.");

            var createdPost = await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(ViewPost), new { id = createdPost.PostID }, createdPost);
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditPost(int id, [FromBody] Post updatedPost)
        {
            var post = await _postService.UpdatePostAsync(id, updatedPost);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (!result)
                return NotFound("Post not found.");

            return NoContent();
        }

        [HttpPost("toggle-favourite/{postId}")]
        public async Task<IActionResult> ToggleFavorite(int postId, [FromBody] int userId)
        {
            var favourite = await _postService.ToggleFavoriteAsync(postId, userId);
            return Ok(new { message = favourite == null ? "Added to favorites" : "Removed from favorites", postId });
        }

        [HttpPost("join-trip/{postId}")]
        public async Task<IActionResult> JoinTrip(int postId, [FromBody] int userId)
        {
            var result = await _postService.JoinTripAsync(postId, userId);
            if (result == null)
                return BadRequest(new { message = "Failed to join the trip." });

            return Ok(new { message = "Successfully joined the trip", postId });
        }
    }
}
