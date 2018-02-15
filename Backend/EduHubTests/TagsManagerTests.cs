using EduHubLibrary.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public void TryToFindExistingTagsWithPartOfWord_GetListOfFoundTags()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Teg3");

            //Act
            var expectedTags = new List<string> { "Tag1", "Tag2" };
            var actualTags = tagsManager.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0], actualTags[0]);
            Assert.AreEqual(expectedTags[1], actualTags[1]);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }

        [TestMethod]
        public void TryToFindNotExistingTagsWithPartOfWord_GetEmptyList()
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
    }
}
