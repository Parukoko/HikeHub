using HikeHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public interface IUserService
    {
        Task<List<Post>> GetUserByIdAsync(int id);
	}
}
