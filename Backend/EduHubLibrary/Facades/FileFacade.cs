using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubLibrary.Facades
{
    public class FileFacade : IFileFacade
    {
        public void AddFile(string filename, string contentType)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            Ensure.String.IsNotNullOrWhiteSpace(contentType);
            UserFile userFile = new UserFile(filename, contentType);
            _fileRepository.AddFile(userFile);
        }

        public UserFile GetFile(string filename)
        {
            Ensure.String.IsNotNullOrWhiteSpace(filename);
            return _fileRepository.GetFile(filename);
        }

        public FileFacade(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        private readonly IFileRepository _fileRepository;
    }
}
