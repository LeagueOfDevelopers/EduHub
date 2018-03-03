using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class TagFacadeTests
    {
        [TestMethod]
        public void TryToFindExistingTags_GetListOfFoundTags()
        {
            //Arrange
            var tagRepository = new InMemoryTagRepository();
            tagRepository.Add("Tag1");
            tagRepository.Add("Tag2");
            tagRepository.Add("Teg3");
            var tagFacade = new TagFacade(tagRepository);

            //Act
            var expectedTags = new List<string> {"Tag1", "Tag2"};
            var actualTags = tagFacade.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0], actualTags[0]);
            Assert.AreEqual(expectedTags[1], actualTags[1]);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }

        [TestMethod]
        public void TryToFindNotExistingTags_GetEmptyList()
        {
            //Arrange
            var tagRepository = new InMemoryTagRepository();
            tagRepository.Add("Tag1");
            tagRepository.Add("Tag2");
            tagRepository.Add("Teg3");
            var tagFacade = new TagFacade(tagRepository);

            //Act
            var actualTags = tagFacade.FindTag("Teg1").ToList();

            //Assert
            Assert.AreEqual(0, actualTags.Count);
        }

        [TestMethod]
        public void UseNewTag_GetAddedNewTag()
        {
            //Arrange
            var tagRepository = new InMemoryTagRepository();
            var tagFacade = new TagFacade(tagRepository);

            //Act
            tagFacade.UseTag("Tag");

            //Assert
            Assert.AreEqual(1, tagFacade.FindTag("Tag").Count());
        }

        [TestMethod]
        public void UseExistingTag_GetUpdatedPopularity()
        {
            //Arrange
            var tagRepository = new InMemoryTagRepository();
            tagRepository.Add("Tag1");
            tagRepository.Add("Tag2");
            var tagFacade = new TagFacade(tagRepository);

            //Act
            tagFacade.UseTag("Tag2");

            //Assert
            var expectedTags = new List<string> { "Tag2", "Tag1" };
            var actualTags = tagFacade.FindTag("Tag").ToList();
            
            Assert.AreEqual(expectedTags[0], actualTags[0]);
            Assert.AreEqual(expectedTags[1], actualTags[1]);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }

        [TestMethod]
        public void LookingForTagAmongTagsWithDifferentPopularity_GetSortedResultList()
        {
            //Arrange
            var tagRepository = new InMemoryTagRepository();
            tagRepository.Add("Tag1");
            tagRepository.Add("Tag2");
            tagRepository.Add("Tag3");
            var tagFacade = new TagFacade(tagRepository);

            tagFacade.UseTag("Tag1");

            tagFacade.UseTag("Tag2");
            tagFacade.UseTag("Tag2");
            tagFacade.UseTag("Tag2");

            tagFacade.UseTag("Tag3");
            tagFacade.UseTag("Tag3");

            //Act
            var expectedTags = new List<string> {"Tag2", "Tag3", "Tag1"};
            var actualTags = tagFacade.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0], actualTags[0]);
            Assert.AreEqual(expectedTags[1], actualTags[1]);
            Assert.AreEqual(expectedTags[2], actualTags[2]);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }
    }
}