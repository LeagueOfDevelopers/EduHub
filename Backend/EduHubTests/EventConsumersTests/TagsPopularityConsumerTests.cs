using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Domain.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubTests.EventConsumersTests
{
    [TestClass]
    public class TagsPopularityConsumerTests
    {
        [TestMethod]
        public void UseNewTag_GetAddedTag()
        {
            //Arrange
            var tagsManager = new TagsManager();
            var consumer = new TagPopularityConsumer(tagsManager);

            //Act
            consumer.Consume(new UsingTagEvent("New tag"));
            var actual = tagsManager.Tags.ToList()[0].Name;

            //Assert
            Assert.AreEqual(1, tagsManager.Tags.Count());
            Assert.AreEqual("New tag", actual);
        }

        [TestMethod]
        public void UseExistingTag_GetUpdatedPopularityOfTag()
        {
            //Arrange
            var tagsManager = new TagsManager();
            var consumer = new TagPopularityConsumer(tagsManager);
            consumer.Consume(new UsingTagEvent("New tag"));

            //Act
            consumer.Consume(new UsingTagEvent("New tag"));
            var expectedPopularity = 2;
            var actualPopularity = tagsManager.Tags.ToList()[0].Popularity;

            //Assert
            Assert.AreEqual(1, tagsManager.Tags.Count());
            Assert.AreEqual(expectedPopularity, actualPopularity);
        }
    }
}
