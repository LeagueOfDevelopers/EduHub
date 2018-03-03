using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Domain
{
    public class TagFacade : ITagFacade
    {
        public TagFacade(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public IEnumerable<string> FindTag(string tag)
        {
            return _tagRepository.Find(tag);
        }
        
        public void UseTag(string tag)
        {
            if (_tagRepository.DoesExist(tag))
            {
                var currentTag = _tagRepository.Get(tag);
                currentTag.AddPopularity();
                _tagRepository.Update(currentTag);
            }
            else _tagRepository.Add(tag);
        }

        private readonly ITagRepository _tagRepository;
    }
}