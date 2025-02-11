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

        // Method to create a post
        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        // Method to get a post by ID
        public async Task<Post> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .FirstOrDefaultAsync(p => p.PostID == id);

            return post;
        }

        // Method to update a post
        public async Task<Post> UpdatePostAsync(int id, Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return null;

            // Update the properties of the post
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

        // Method to delete a post
        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
                return false;

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }

        // Method to get all posts
        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .ToListAsync();
        }
    }
}
