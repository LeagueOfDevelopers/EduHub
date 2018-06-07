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
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Review");
            });
            modelBuilder.Entity<ContactDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Contact");
            });
            modelBuilder.Entity<InvitationDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Invitation");
            });
            modelBuilder.Entity<NotifiesDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Notifie");
            });
            modelBuilder.Entity<UserDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("User");
            });
            modelBuilder.Entity<TagDto>(e =>
            {
                e.Property(entity => entity.Name).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Tag");
            });
            modelBuilder.Entity<UserFileDto>(e =>
            {
                e.Property(entity => entity.Filename).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("File");
            });
            modelBuilder.Entity<KeyDto>(e =>
            {
                e.Property(entity => entity.Value).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("KeyData");
            });
            modelBuilder.Entity<GroupDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("GroupData");
            });
            modelBuilder.Entity<SanctionDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Sanction");
            });
            modelBuilder.Entity<MemberDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Member");
            });
            modelBuilder.Entity<MessageDto>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("Message");
            });
            modelBuilder.Entity<TagGroup>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("TagGroup");
            });
            modelBuilder.Entity<TagUser>(e =>
            {
                e.Property(entity => entity.Id).HasMaxLength(127);
                e.ForMySQLHasCollation("utf8mb4_bin");
                e.ToTable("TagUser");
            });
        }
    }
}