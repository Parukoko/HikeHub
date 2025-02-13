using Microsoft.EntityFrameworkCore;
using HikeHub.Models;
using System;
using System.Threading.Tasks;

namespace HikeHub.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly HikeHubDbContext _context;

        public AnnouncementService(HikeHubDbContext context)
        {
            _context = context;
        }

        public async Task<Announcement> CreateAnnouncementAsync(Announcement announcement)
        {
            if (announcement == null)
                throw new ArgumentNullException(nameof(announcement), "Announcement cannot be null.");

            try
            {
                var newAnnouncement = new Announcement
                {
                    Title = announcement.Title ?? throw new ArgumentException("Title is required", nameof(announcement.Title)),
                    Description = announcement.Description ?? throw new ArgumentException("Description is required", nameof(announcement.Description)),
                    Image = announcement.Image
                };

                var result = await _context.Announcements.AddAsync(newAnnouncement);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating announcement: {ex.Message}");
                throw;
            }
        }
		public async Task<Announcement> GetAnnouncementById(int id)
		{
			var announcement = await _context.Announcements
				.Include(a => a.Participants)
				.Include(a => a.User)
				.FirstOrDefaultAsync(a => a.AnnouncementID == id);

			if (announcement == null)
			{
				throw new ArgumentNullException(nameof(announcement), "Announcement is null.");
			}

			return announcement;
		}
    }
}
