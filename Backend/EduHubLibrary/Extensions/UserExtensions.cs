using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using Newtonsoft.Json;

namespace EduHubLibrary.Extensions
{
    internal static class UserExtensions
    {
        public static User ParseFromUserDto(UserDto source)
        {
            var invitationList = new List<Invitation>();
            var notifiesList = new List<Notification>();
            var skillList = new List<string>();
            var reviewList = new List<Review>();
            var contactList = new List<string>();

            source.Invitations?.ForEach(i => invitationList.Add(
                new Invitation(i.FromUser, i.ToUser, i.GroupId, i.SuggestedRole, i.Status, i.Id)));

            source.Notifies?.ForEach(n => notifiesList.Add(new Notification(n.OccurredOn,
                n.NotificationInfo, n.NotificationType)));

            source.Reviews?.ForEach(r =>
                reviewList.Add(new Review(r.FromUser, r.Title, r.Text, r.FromGroup, r.Id)));

            source.Tags?.ToList().ForEach(t => skillList.Add(t.Name));

            source.Contacts?.ForEach(c => contactList.Add(c.Contact));


            var teacherProfile = new TeacherProfile(reviewList, skillList);
            var userProfile = new UserProfile(source.Name, source.Email, source.AboutUser, source.BirthYear,
                source.Gender, source.IsTeacher, source.AvatarLink, contactList);

            var settings = JsonConvert.DeserializeObject<NotificationsSettings>(source.NotificationSettings);

            var destination = new User(source.Name, new Credentials(source.Email, source.PasswordHash),
                source.Type, invitationList, teacherProfile, userProfile, source.IsActive, notifiesList, settings, 
                source.Id);

            return destination;
        }
    }
}