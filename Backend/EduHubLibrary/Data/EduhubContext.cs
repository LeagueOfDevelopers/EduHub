using EduHubLibrary.Data.FileDtos;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.KeyDtos;
using EduHubLibrary.Data.SanctionDtos;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

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
            modelBuilder.Entity<ReviewDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Review");
            });
            modelBuilder.Entity<ContactDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Contact");
            });
            modelBuilder.Entity<InvitationDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Invitation");
            });
            modelBuilder.Entity<NotifiesDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Notifie");
            });
            modelBuilder.Entity<UserDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("User");
            });
            modelBuilder.Entity<TagDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Tag");
            });
            modelBuilder.Entity<UserFileDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("File");
            });
            modelBuilder.Entity<KeyDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Key");
            });
            modelBuilder.Entity<GroupDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Group");
            });
            modelBuilder.Entity<SanctionDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Sanction");
            });
            modelBuilder.Entity<MemberDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Member");
            });
            modelBuilder.Entity<MessageDto>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("Message");
            });
            modelBuilder.Entity<TagGroup>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("TagGroup");
            });
            modelBuilder.Entity<TagUser>(e =>
            {
                e.ForMySQLHasCollation("utf8_general_ci");
                e.ToTable("TagUser");
            });
        }
    }
}