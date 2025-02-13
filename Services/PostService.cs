using Microsoft.EntityFrameworkCore;
using HikeHub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            var newPost = await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return newPost.Entity;
        }

        public async Task<List<Post>> GetPostsByTitleAsync(string title)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .Where(p => p.Title.Contains(title))
                .ToListAsync();
        }
        public async Task<List<Post>> GetPostsByUsernameAsync(string username)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Where(p => p.User != null && p.User.UserName == username)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByTagAsync(string tag)
        {
            if (!Enum.TryParse<Tag.TagType>(tag, out var tagEnum))
            {
                throw new ArgumentException($"Invalid tag type: {tag}");
            }

            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .Where(p => p.TagList.Any(t => t.TagName == tagEnum))
                .ToListAsync();
        }

        public async Task<Post?> GetPostByPostIdAsync(int postId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .FirstOrDefaultAsync(p => p.PostID == postId);
        }

        public async Task<Post?> GetPostByAnnouncementIdAsync(int announcementId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .FirstOrDefaultAsync(p => p.AnnouncementID == announcementId);
        }

        public async Task<Post?> UpdatePostAsync(int id, Post updatedPost)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return null;

            post.Title = updatedPost.Title;
            post.MeetingLocation = updatedPost.MeetingLocation;
            post.MeetingProvince = updatedPost.MeetingProvince;
            post.DestinationAddress = updatedPost.DestinationAddress;
            post.Duration = updatedPost.Duration;
            post.Description = updatedPost.Description;
            post.MaxParticipants = updatedPost.MaxParticipants;
            post.DepartedDate = updatedPost.DepartedDate;
            post.ReturnedDate = updatedPost.ReturnedDate;
            post.ExpiredDate = updatedPost.ExpiredDate;
            post.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await GetPostByPostIdAsync(id);
            if (post == null) return false;

            try
            {
                if (post.Favourites != null && post.Favourites.Any())
                {
                    _context.Favourites.RemoveRange(post.Favourites);
                }

                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.TagList)
                .ToListAsync();
        }
        public async Task<Favourite?> ToggleFavoriteAsync(int postId, int userId)
        {
            var favourite = await _context.Favourites
                .SingleOrDefaultAsync(f => f.UserID == userId && f.PostID == postId);

            if (favourite == null)
            {
                var newFavourite = new Favourite { UserID = userId, PostID = postId };
                _context.Favourites.Add(newFavourite);
                await _context.SaveChangesAsync();
                return newFavourite;
            }
            else
            {
                _context.Favourites.Remove(favourite);
                await _context.SaveChangesAsync();
                return null;
            }
        }

        public async Task<Participant?> JoinTripAsync(int postId, int userId)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post == null || post.MaxParticipants <= 0) return null;

            var existingParticipant = await _context.Participants
                .FirstOrDefaultAsync(p => p.UserID == userId && p.PostID == postId);

            if (existingParticipant != null) return null;

            var newParticipant = new Participant { UserID = userId, PostID = postId };
            _context.Participants.Add(newParticipant);
            post.MaxParticipants--;

            await _context.SaveChangesAsync();
            return newParticipant;
        }

    }
}
