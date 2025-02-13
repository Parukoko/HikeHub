using HikeHub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public interface IAnnouncementService
    {
        Task<Announcement> CreateAnnouncementAsync(Announcement announcement);
		Task<Announcement> GetAnnouncementById(int id);
	}
}
