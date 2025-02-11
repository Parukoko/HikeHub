using Microsoft.EntityFrameworkCore;
using HikeHub.Models;

namespace HikeHub.Models
{
    public class HikeHubDbContext : DbContext
    {
        public HikeHubDbContext(DbContextOptions<HikeHubDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HikeHub;Username=postgres;Password=1234567890");
        }
    }
}
