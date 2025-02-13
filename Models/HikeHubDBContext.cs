using Microsoft.EntityFrameworkCore;
using HikeHub.Models;

namespace HikeHub.Models
{
    public class HikeHubDbContext : DbContext
    {
        public HikeHubDbContext(DbContextOptions<HikeHubDbContext> options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tag>().ToTable("tags");
            modelBuilder.Entity<Tag>()
                .HasOne(t => t.Post)
                .WithMany(p => p.TagList)
                .HasForeignKey(t => t.PostID);
            modelBuilder.Entity<Post>()
                .HasOne<User>(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserID)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Favourite>()
                .HasKey(pl => new { pl.UserID, pl.PostID });
            modelBuilder.Entity<Favourite>()
                .HasOne<User>(pl => pl.User)
                .WithMany(u => u.Favourites)
                .HasForeignKey(pl => pl.UserID);
            modelBuilder.Entity<Participant>()
                .HasKey(p => new { p.UserID, p.AnnouncementID });
            modelBuilder.Entity<Participant>()
                .HasOne<User>(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.UserID);
            modelBuilder.Entity<Participant>()
                .HasOne<Announcement>(p => p.Announcement)
                .WithMany(a => a.Participants)
                .HasForeignKey(p => p.AnnouncementID);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HikeHub;Username=postgres;Password=1234567890");
        }
    }
}
