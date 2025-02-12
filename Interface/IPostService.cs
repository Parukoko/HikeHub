using HikeHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public interface IPostService
    {
        Task<Post> CreatePostAsync(Post post);
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> UpdatePostAsync(int id, Post updatedPost);
        Task<bool> DeletePostAsync(int id);
        Task<List<Post>> GetAllPostsAsync();
    }
}
