using EduHubLibrary.Data.FileDtos;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.KeyDtos;
using EduHubLibrary.Data.SanctionDtos;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;
using Microsoft.EntityFrameworkCore;

namespace EduHubLibrary.Data
{
    public class EduhubContext : DbContext
    {
        private readonly string _сonnectionString;

        public EduhubContext(string connectionString)
        {
            _сonnectionString = connectionString;
        }

        public DbSet<UserDto> Users { get; set; }
        public DbSet<TagDto> Tags { get; set; }
        public DbSet<UserFileDto> Files { get; set; }
        public DbSet<KeyDto> Keys { get; set; }
        public DbSet<GroupDto> Groups { get; set; }
        public DbSet<TagGroup> TagGroup { get; set; }
        public DbSet<SanctionDto> Sanctions { get; set; }

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
            modelBuilder.Entity<SanctionDto>().ToTable("Sanction");
        }
    }
}