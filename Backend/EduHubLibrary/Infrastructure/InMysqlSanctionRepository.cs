using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Data;
using EduHubLibrary.Data.SanctionDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Extensions;
using EnsureThat;

namespace EduHubLibrary.Infrastructure
{
    public class InMysqlSanctionRepository : ISanctionRepository
    {
        private readonly string _connectionString;

        public InMysqlSanctionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Sanction sanction)
        {
            using (var context = new EduhubContext(_connectionString))
            {
                var sanctionDto = new SanctionDto();
                sanctionDto.ParseFromSanction(sanction);
                context.Sanctions.Add(sanctionDto);
                context.SaveChanges();
                sanction.Id = sanctionDto.Id;
            }
        }

        public Sanction Get(int id)
        {
            using (var context = new EduhubContext(_connectionString))
            {
                var sanctionDto = context.Sanctions
                    .FirstOrDefault(s => s.Id == id);
                Ensure.Any.IsNotNull(sanctionDto, nameof(sanctionDto),
                    opt => opt.WithException(new SanctionNotFoundException(id)));
                var result = SanctionExtensions.ParseFromSanctionDto(sanctionDto);
                return result;
            }
        }

        public IEnumerable<Sanction> GetAll()
        {
            using (var context = new EduhubContext(_connectionString))
            {
                var sanctionDto = context.Sanctions.ToList();
                var result = new List<Sanction>();
                sanctionDto.ForEach(dto => result.Add(SanctionExtensions.ParseFromSanctionDto(dto)));
                return result;
            }
        }

        public IEnumerable<Sanction> GetAllOfUser(int userId)
        {
            using (var context = new EduhubContext(_connectionString))
            {
                var sanctionDto = context.Sanctions.Where(s => s.UserId == userId).ToList();
                var result = new List<Sanction>();
                sanctionDto.ForEach(dto => result.Add(SanctionExtensions.ParseFromSanctionDto(dto)));
                return result;
            }
        }

        public IEnumerable<Sanction> GetAllActive()
        {
            using (var context = new EduhubContext(_connectionString))
            {
                var sanctionDto = context.Sanctions.Where(s => s.IsActive).ToList();
                var result = new List<Sanction>();
                sanctionDto.ForEach(dto => result.Add(SanctionExtensions.ParseFromSanctionDto(dto)));
                return result;
            }
        }

        public void Update(Sanction sanction)
        {
            using (var context = new EduhubContext(_connectionString))
            {
                var sanctionDto = context.Sanctions.FirstOrDefault(s => s.Id == sanction.Id);
                sanctionDto.ParseFromSanction(sanction);
                context.SaveChanges();
            }
        }
    }
}