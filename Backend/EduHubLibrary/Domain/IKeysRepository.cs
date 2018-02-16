using System;

namespace EduHubLibrary.Domain
{
    public interface IKeysRepository
    {
        void AddKey(Key key);
        Key GetKey(Guid keyId);
    }
}