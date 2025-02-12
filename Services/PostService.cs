using Microsoft.EntityFrameworkCore;
using HikeHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public class PostService : IPostService
    {
        private readonly HikeHubDbContext _context;

        public PostService(HikeHubDbContext context)
        {
            _context = context;
        }

        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .FirstOrDefaultAsync(p => p.PostID == id);

            if (post == null)
            {
                throw new KeyNotFoundException($"Post with ID {id} not found.");
            }

            return post;
        }

        public List<Post> GetPostsByUsername(string username)
        {
            return _context.Posts
                .Include(p => p.User)
                .Where(p => p.User != null && p.User.UserName == username)
                .ToList();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts
                .Include(p => p.User)
                .Include(p => p.Favourites).ThenInclude(f => f.User)
                .Include(p => p.TagList)
                .ToList();
        }
        public async Task<Post> UpdatePostAsync(int id, Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                throw new KeyNotFoundException($"Post with ID {id} not found.");

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
            return post;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .ToListAsync();
            return posts ?? new List<Post>();
        }
    }
}
