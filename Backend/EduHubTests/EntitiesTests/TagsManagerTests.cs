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
    public class TagsManagerTests
    {
        [TestMethod]
        public void TryToFindExistingTags_GetListOfFoundTags()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Teg3");

            //Act
            var expectedTags = new List<string> {"Tag1", "Tag2"};
            var actualTags = tagsManager.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0], actualTags[0]);
            Assert.AreEqual(expectedTags[1], actualTags[1]);
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
        public void LookingForTagAmongTagsWithDifferentPopularity_GetSortedResultList()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Tag3");

            tagsManager.AddPopularity("Tag1");

            tagsManager.AddPopularity("Tag2");
            tagsManager.AddPopularity("Tag2");
            tagsManager.AddPopularity("Tag2");

            tagsManager.AddPopularity("Tag3");
            tagsManager.AddPopularity("Tag3");

            //Act
            var expectedTags = new List<string> {"Tag2", "Tag3", "Tag1"};
            var actualTags = tagsManager.FindTag("Tag").ToList();

            //Assert
            Assert.AreEqual(expectedTags[0], actualTags[0]);
            Assert.AreEqual(expectedTags[1], actualTags[1]);
            Assert.AreEqual(expectedTags[2], actualTags[2]);
            Assert.AreEqual(expectedTags.Count, actualTags.Count);
        }
    }
}