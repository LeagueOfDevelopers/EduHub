using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Settings
{
    public class GroupSettings
    {
        public int MinGroupSize { get; }
        public int MaxGroupSize { get; }
        public double MinGroupValue { get; }
        public double MaxGroupValue { get; }

        public GroupSettings(int minGroupSize, int maxGroupSize, double minGroupValue, double maxGroupValue)
        {
            MinGroupSize = minGroupSize;
            MaxGroupSize = maxGroupSize;
            MinGroupValue = minGroupValue;
            MaxGroupValue = maxGroupValue;
        }
    }
}
