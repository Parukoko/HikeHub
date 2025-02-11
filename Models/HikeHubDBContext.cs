using Microsoft.EntityFrameworkCore;

namespace HikeHub.Models
{
    public class HikeHubDbContext : DbContext
    {
        public HikeHubDbContext(DbContextOptions<HikeHubDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
