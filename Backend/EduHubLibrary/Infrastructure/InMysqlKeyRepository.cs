using System.Linq;
using EduHubLibrary.Data;
using EduHubLibrary.Domain;
using EduHubLibrary.Extensions;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlKeyRepository : IKeysRepository
    {
        private readonly string _connectionString;

        public InMysqlKeyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddKey(Key key)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var keyDto = KeyDtoExtensions.ParseFromKey(key);
                _context.Keys.Add(keyDto);
                _context.SaveChanges();
                key.Value = keyDto.Value;
            }
        }

        public Key GetKey(int keyId)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var keyDto = _context.Keys.FirstOrDefault(k => k.Value == keyId);
                var key = KeyExtensions.ParseFromKeyDto(keyDto);
                return key;
            }
        }

        public void UpdateKey(Key key)
        {
            using (var _context = new EduhubContext(_connectionString))
            {
                var keyDto = _context.Keys.FirstOrDefault(k => k.Value == key.Value);
                keyDto.UpdateKey(key);
                _context.SaveChanges();
            }
        }
    }
}