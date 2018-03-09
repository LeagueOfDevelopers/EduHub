using EduHubLibrary.Data.UserDtos;
using Microsoft.EntityFrameworkCore;

namespace EduHubLibrary.Data
{
    public class EduhubContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        private readonly string _сonnectionString;

        public EduhubContext(string connectionString)
        {
            _сonnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_сonnectionString);
        }
    }
}
