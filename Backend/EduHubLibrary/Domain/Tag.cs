namespace EduHubLibrary.Domain.Tools
{
    public class Tag
    {
        public Tag(string name)
        {
            Name = name;
            Popularity = 1;
        }

        //for db
        internal Tag(string name, int popularity)
        {
            Name = name;
            Popularity = popularity;
        }

        public string Name { get; }
        public int Popularity { get; private set; }

        internal void AddPopularity()
        {
            Popularity++;
        }
    }
}