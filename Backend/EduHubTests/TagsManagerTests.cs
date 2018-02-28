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
        private Guid _creatorId;
        private GroupSettings _groupSettings;
        private InMemoryGroupRepository _inMemoryGroupRepository;
        private InMemoryUserRepository _inMemoryUserRepository;

        [TestInitialize]
        public void Initialize()
        {
            _inMemoryUserRepository = new InMemoryUserRepository();
            _inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeyRepository = new InMemoryKeysRepository();
            _groupSettings = new GroupSettings(2, 10, 0, 1000);
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            var userFacade = new UserFacade(_inMemoryUserRepository, _inMemoryGroupRepository);
            var authUserFacade = new AuthUserFacade(inMemoryKeyRepository, _inMemoryUserRepository, emailSender);
            _creatorId = authUserFacade.RegUser("Alena", new Credentials("email", "password"), true, UserType.User);
        }

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
        public void UseTagInCreatingOfGroup_PopularityWasIncremented()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag");
            var groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, _groupSettings,
                tagsManager);

            //Act
            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            var tag = tagsManager.Tags.First(t => t.Name == "Tag");

            //Assert
            Assert.AreEqual(1, tag.Popularity);
        }

        [TestMethod]
        public void UseTagInEditingOfGroup_PopularityWasIncremented()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            var groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, _groupSettings,
                tagsManager);
            var groupEditFacade = new GroupEditFacade(_inMemoryGroupRepository, _groupSettings, tagsManager);

            //Act
            var createdGroupId = groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag1"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            groupEditFacade.ChangeGroupTags(createdGroupId, _creatorId, new List<string> {"Tag2"});
            var tag = tagsManager.Tags.First(t => t.Name == "Tag2");

            //Assert
            Assert.AreEqual(1, tag.Popularity);
        }

        [TestMethod]
        public void LookingForTagAmongTagsWithDifferentPopularity_GetSortedResultList()
        {
            //Arrange
            var tagsManager = new TagsManager();
            tagsManager.AddTag("Tag1");
            tagsManager.AddTag("Tag2");
            tagsManager.AddTag("Tag3");
            var groupFacade = new GroupFacade(_inMemoryGroupRepository, _inMemoryUserRepository, _groupSettings,
                tagsManager);

            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag1"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);

            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag2"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag2"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag2"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);

            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag3"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);
            groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"Tag3"},
                "You're welcome!", 3, 20, false, GroupType.Lecture);

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