using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Interators
{
    internal static class IntIterator
    {
        private static int _id;

        internal static int GetNextId()
        {
            _id++;
            return _id;
        }
    }
}
