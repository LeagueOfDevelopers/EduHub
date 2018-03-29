using System.Collections.Generic;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Domain
{
    public class TagFacade : ITagFacade
    {
        private readonly ITagRepository _tagRepository;

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
            else
            {
                _tagRepository.Add(tag);
            }
        }
    }
}