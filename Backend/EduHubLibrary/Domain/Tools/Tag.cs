using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Tools
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
            Popularity = 0;
        }

        public string Name { get; }
        public int Popularity { get; private set; }

        internal void AddPopularity()
        {
            Popularity++;
        }
    }
}
