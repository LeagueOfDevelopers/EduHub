using System.Linq;
using EduHubLibrary.Data.Connections;
using EduHubLibrary.Data.FileDtos;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.KeyDtos;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;
using Microsoft.EntityFrameworkCore;

namespace EduHubLibrary.Data
{
    public class EduhubContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<TagDto> Tags { get; set; }
        public DbSet<UserFileDto> Files { get; set; }
        public DbSet<KeyDto> Keys { get; set; }
        public DbSet<GroupDto> Groups { get; set; }
        public DbSet<TagGroup> TagGroup { get; set; }

        private readonly string _сonnectionString;

        public EduhubContext(string connectionString)
        {
            _сonnectionString = connectionString;
            Database.EnsureCreated();
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_сonnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReviewDto>().ToTable("Review");
            modelBuilder.Entity<ContactDto>().ToTable("Contact");
            modelBuilder.Entity<InvitationDto>().ToTable("Invitation");
            modelBuilder.Entity<NotifiesDto>().ToTable("Notifie");
            modelBuilder.Entity<UserDto>().ToTable("User");
            modelBuilder.Entity<TagDto>().ToTable("Tag");
            modelBuilder.Entity<UserFileDto>().ToTable("File");
            modelBuilder.Entity<KeyDto>().ToTable("Key");
            modelBuilder.Entity<GroupDto>().ToTable("Group");
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();
            foreach (var entity in changedEntriesCopy)
            {
                this.Entry(entity.Entity).State = EntityState.Detached;
            }
        }
    }
}
