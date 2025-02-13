using Microsoft.AspNetCore.Identity;
using HikeHub.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HikeHub.ViewModels;



namespace HikeHub.Services
{
    public class UserService: IUserService
    {
        private readonly HikeHubDbContext _context;

        public UserService(HikeHubDbContext context)
        {
            _context = context;

        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

		public async Task<User> GetUserByIdAsync(int id)
		{
			var user = await _context.Users
				.Include(u => u.Posts)
				.Include(u => u.Favourites!.AsQueryable())
					.ThenInclude(f => f.Post)
				.FirstOrDefaultAsync(u => u.UserID == id);

			return user ?? throw new ArgumentException("User not found.");
		}

		public User? GetUserById(int id)
		{
			var user = _context.Users
				.Include(u => u.Posts)
				.Include(u => u.Favourites!.AsQueryable())
					.ThenInclude(f => f.Post)
				.FirstOrDefault(u => u.UserID == id);

			return user ?? throw new ArgumentException("User not found.");
		}

        Task<List<Post>> IUserService.GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
