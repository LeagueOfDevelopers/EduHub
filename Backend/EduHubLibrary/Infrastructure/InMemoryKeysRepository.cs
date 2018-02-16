using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EnsureThat;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryKeysRepository : IKeysRepository
    {
        private readonly List<Key> keyList;

        public InMemoryKeysRepository(List<Key> keyList)
        {
            this.keyList = keyList;
        }

        public InMemoryKeysRepository()
        {
            keyList = new List<Key>();
        }

        public void AddKey(Key key)
        {
            Ensure.Any.IsNotNull(key);
            keyList.Add(key);
        }

        public Key GetKey(Guid keyId)
        {
            Ensure.Guid.IsNotEmpty(keyId);
            var key = keyList.FirstOrDefault(k => k.Value == keyId);
            Ensure.Any.IsNotNull(key);
            return key;
        }
    }
}