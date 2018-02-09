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
            _fileList.Add(file);
        }

        public bool DoesFileExists(string filename)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            return _fileList.Any(f => f.Filename.Equals(filename));
        }

        public UserFile GetFile(string filename)
        {
            if (!DoesFileExists(filename))
                throw new FileNotFoundException(filename);
            return _fileList.First(f => f.Filename.Equals(filename));
        }

        public InMemoryFileRepository()
        {
            _fileList = new List<UserFile>();
        }

        public InMemoryFileRepository(List<UserFile> _fileList)
        {
            this._fileList = _fileList;
        }

        private List<UserFile> _fileList;
    }
}
