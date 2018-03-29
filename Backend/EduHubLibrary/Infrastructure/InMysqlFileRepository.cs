using System.Linq;
using EduHubLibrary.Data;
using EduHubLibrary.Data.FileDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Extensions;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlFileRepository : IFileRepository
    {
        private readonly string _connectionString;

        public InMysqlFileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddFile(UserFile file)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userFileDto = new UserFileDto(file.Filename, file.ContentType);
                _context.Files.Add(userFileDto);
                _context.SaveChanges();
            }
        }

        public bool DoesFileExists(string filename)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                return _context.Files.Any(f => f.Filename == filename);
            }
        }

        public UserFile GetFile(string filename)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var userFileDto = _context.Files.FirstOrDefault(f => f.Filename == filename);
                return UserFileExtensions.ParseFromUserFileDto(userFileDto);
            }
        }
    }
}