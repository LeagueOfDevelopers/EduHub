using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EduHubLibrary.Interators;
using System.Linq;

namespace EduHubLibrary.Infrastructure
{
    public class InMemorySanctionRepository : ISanctionRepository
    {
        public InMemorySanctionRepository()
        {
            _listOfSanctions = new List<Sanction>();
        }

        public void Add(Sanction sanction)
        {
            if (sanction == null)
                throw new ArgumentNullException();
            sanction.Id = IntIterator.GetNextId();
            _listOfSanctions.Add(sanction);
        }

        public Sanction Get(int id)
        {
            return _listOfSanctions.First(s => s.Id == id);
        }

        public IEnumerable<Sanction> GetAll()
        {
            return _listOfSanctions;
        }

        public IEnumerable<Sanction> GetAllOfModerator(int moderatorId)
        {
            return _listOfSanctions.FindAll(s => s.ModeratorId == moderatorId);
        }

        public IEnumerable<Sanction> GetAllOfUser(int userId)
        {
            return _listOfSanctions.FindAll(s => s.UserId == userId);
        }

        public void Update(Sanction sanction)
        {
            if (sanction == null)
                throw new ArgumentNullException();
            var currentSanction = _listOfSanctions.Find(current => current.Id == sanction.Id) ??
                             throw new Exception("TODO");
            currentSanction = sanction;
        }

        private readonly List<Sanction> _listOfSanctions;
    }
}
