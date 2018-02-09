using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IFileFacade
    {
        UserFile GetFile(string filename);
        void AddFile(string filename, string contentType);
    }
}
