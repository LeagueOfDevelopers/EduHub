namespace EduHubLibrary.Domain
{
    public interface IFileRepository
    {
        void AddFile(UserFile file);
        bool DoesFileExists(string filename);
        UserFile GetFile(string filename);
    }
}