using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryFileRepository : IFileRepository
    {
        public void AddFile(UserFile file)
        {
            Ensure.Any.IsNotNull(file);
            fileList.Add(file);
        }

        public bool DoesFileExists(string filename)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            return fileList.Any(f => f.Filename.Equals(filename));
        }

        public UserFile GetFile(string filename)
        {
            if (!DoesFileExists(filename))
                throw new FileNotFoundException(filename);
            return fileList.First(f => f.Filename.Equals(filename));
        }

        public InMemoryFileRepository()
        {
            fileList = new List<UserFile>();
        }

        public InMemoryFileRepository(List<UserFile> _fileList)
        {
            fileList = _fileList;
        }

        private List<UserFile> fileList;
    }
}
