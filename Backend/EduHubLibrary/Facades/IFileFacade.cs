using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades
{
    public interface IFileFacade
    {
        UserFile GetFile(string filename);
        void AddFile(string filename, string contentType);
    }
}