using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain.Tools;

namespace EduHubTests
{
    [TestClass]
    public class TagsManagerTests
    {
        [TestMethod]
        public void AddNewTagInTagsManager_GetUpdatedTagsList()
        {
            //Arrange
            var tagsManager = new TagsManager();

            //Act
            tagsManager.AddTag("New tag");

            //Assert
            Assert.AreEqual(1, tagsManager.Tags.Count());
        }

        [TestMethod]
        public void TryToFindExistingTags_GetListOfFoundTags()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Teg3");

            //Act
            var expectedTags = new List<Tag> { new Tag("Tag1"), new Tag("Tag2") };
            var actualTags = tagsManager.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0].Name, actualTags[0].Name);
            Assert.AreEqual(expectedTags[1].Name, actualTags[1].Name);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }

        [TestMethod]
        public void TryToFindNotExistingTags_GetEmptyList()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Teg3");

            //Act
            var actualTags = tagsManager.FindTag("Teg1").ToList();

            //Assert
            Assert.AreEqual(0, actualTags.Count);
        }

        [TestMethod]
        public void FindTagAndCheckItsPopularity_PopularityWasIncremented()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");

            //Act
            tagsManager.FindTag("Tag1");
            var tag1 = tagsManager.Tags.First(t => t.Name == "Tag1");

            //Assert
            Assert.AreEqual(1, tag1.Popularity);
        }

        [TestMethod]
        public void LookingForTagAmongTagsWithDifferentPopularity_GetSortedResultList()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Tag3");

            tagsManager.FindTag("Tag1");

            tagsManager.FindTag("Tag2");
            tagsManager.FindTag("Tag2");
            tagsManager.FindTag("Tag2");

            tagsManager.FindTag("Tag3");
            tagsManager.FindTag("Tag3");

            //Act
            var expectedTags = new List<Tag> { new Tag("Tag2"), new Tag("Tag3"), new Tag("Tag1") };
            var actualTags = tagsManager.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0].Name, actualTags[0].Name);
            Assert.AreEqual(expectedTags[1].Name, actualTags[1].Name);
            Assert.AreEqual(expectedTags[2].Name, actualTags[2].Name);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }
    }
}