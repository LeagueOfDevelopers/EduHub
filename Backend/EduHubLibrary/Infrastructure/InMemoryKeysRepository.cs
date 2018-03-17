using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Interators;
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
            key.Value = IntIterator.GetNextId();
        }

        public Key GetKey(int keyId)
        {
            var key = keyList.FirstOrDefault(k => k.Value == keyId);
            Ensure.Any.IsNotNull(key);
            return key;
        }

        public void UpdateKey(Key key)
        {
            
        }
    }
}