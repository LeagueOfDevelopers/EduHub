using EduHubLibrary.Domain;
using EnsureThat;

namespace EduHubLibrary.Facades
{
    public class FileFacade : IFileFacade
    {
        private readonly IFileRepository _fileRepository;

        public FileFacade(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public void AddFile(string filename, string contentType)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            Ensure.String.IsNotNullOrWhiteSpace(contentType);
            var userFile = new UserFile(filename, contentType);
            _fileRepository.AddFile(userFile);
        }

        public UserFile GetFile(string filename)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            return _fileRepository.GetFile(filename);
        }
    }
}