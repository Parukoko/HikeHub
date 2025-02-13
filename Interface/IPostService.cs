using HikeHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post);
        Task<List<Post>> GetPostsByTitleAsync(string title);
        Task<List<Post>> GetPostsByUsernameAsync(string username);
        Task<List<Post>> GetPostsByTagAsync(string tag);
        Task<Post?> GetPostByPostIdAsync(int postId);
        Task<Post?> GetPostByAnnouncementIdAsync(int announcementId);
        Task<Post?> UpdatePostAsync(int id, Post updatedPost);
        Task<bool> DeletePostAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
        Task<Favourite?> ToggleFavoriteAsync(int postId, int userId);
        Task<Participant?> JoinTripAsync(int postId, int userId);
    }
}
