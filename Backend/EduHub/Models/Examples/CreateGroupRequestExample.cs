using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Tags = new List<string> { "js", "c#" },
                Size = 3,
                MoneyPerUser = 100,
                GroupType = EduHubLibrary.Domain.GroupType.Lecture,
                IsPrivate = false
            };
        }
    }
}
