namespace EduHubLibrary.Domain.Tools
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
            Popularity = 1;
        }

        public string Name { get; }
        public int Popularity { get; private set; }

        internal void AddPopularity()
        {
            Popularity++;
        }
    }
}