using System.Collections.Generic;
using EduHubLibrary.Domain;
using Swashbuckle.AspNetCore.Examples;

namespace EduHub.Models.Examples
{
    public class CreateGroupRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new CreateGroupRequest
            {
                Title = "some Group",
                Description = "some Description",
                Tags = new List<string> {"js", "c#"},
                Size = 3,
                MoneyPerUser = 100,
                GroupType = GroupType.Lecture,
                IsPrivate = false
            };
        }
    }
}