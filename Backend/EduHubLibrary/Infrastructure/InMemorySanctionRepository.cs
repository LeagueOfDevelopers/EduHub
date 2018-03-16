using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EduHubLibrary.Interators;
using System.Linq;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;

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
            return Ensure.Any.IsNotNull(_listOfSanctions.Find(s => s.Id == id), nameof(Get),
                opt => opt.WithException(new SanctionNotFoundException(id)));
        }

        public IEnumerable<Sanction> GetAll()
        {
            Update();
            return _listOfSanctions;
        }

        public IEnumerable<Sanction> GetAllOfModerator(int moderatorId)
        {
            Update();
            return _listOfSanctions.FindAll(s => s.ModeratorId == moderatorId);
        }

        public IEnumerable<Sanction> GetAllOfUser(int userId)
        {
            Update();
            return _listOfSanctions.FindAll(s => s.UserId == userId);
        }

        public void Update(Sanction sanction)
        {
            if (sanction == null)
                throw new ArgumentNullException();
            var currentSanction = _listOfSanctions.Find(current => current.Id == sanction.Id) ??
                             throw new SanctionNotFoundException(sanction.Id);
            currentSanction = sanction;
        }

        private void Update()
        {
            _listOfSanctions.FindAll(s => s.IsTemporary == true && s.ExpirationDate < DateTimeOffset.Now)
                .ForEach(s => s.Cancel());
        }

        private readonly List<Sanction> _listOfSanctions;
    }
}
