namespace EduHubLibrary.Settings
{
    public class GroupSettings
    {
        public GroupSettings(int minGroupSize, int maxGroupSize, double minGroupValue, double maxGroupValue)
        {
            MinGroupSize = minGroupSize;
            MaxGroupSize = maxGroupSize;
            MinGroupValue = minGroupValue;
            MaxGroupValue = maxGroupValue;
        }

        public int MinGroupSize { get; }
        public int MaxGroupSize { get; }
        public double MinGroupValue { get; }
        public double MaxGroupValue { get; }
    }
}