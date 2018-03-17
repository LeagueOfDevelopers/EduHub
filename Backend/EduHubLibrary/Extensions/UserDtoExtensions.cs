using System.Linq;
using EduHubLibrary.Data.Connections;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Extensions
{
    internal static class UserDtoExtensions
    {
        public static void ParseFromUser(this UserDto destination, User source)
        {
            var result = destination;
            result.Id = source.Id;
            result.IsActive = source.IsActive;
            result.Type = source.Type;
            result.Email = source.Credentials.Email;
            result.PasswordHash = source.Credentials.PasswordHash;
            result.Name = source.UserProfile.Name;
            result.AboutUser = source.UserProfile.AboutUser;
            result.BirthYear = source.UserProfile.BirthYear;
            result.Gender = source.UserProfile.Gender;
            result.IsTeacher = source.UserProfile.IsTeacher;
            result.AvatarLink = source.UserProfile.AvatarLink;

            source.TeacherProfile?.Skills?.ForEach(s =>
            {
                if (result.UserTags.All(userTag => userTag.TagId != s))
                {
                    result.UserTags.Add(new UserTag(s, new TagDto(s), source.Id, result));
                }
            });

            source.TeacherProfile?.Reviews?.ForEach(review =>
            {
                if (result.Reviews.All(reviewDto => reviewDto.Id != review.Id))
                {
                    result.Reviews.Add(new ReviewDto(review.Id, review.FromUser, review.FromGroup,
                        review.Title, review.Text, review.Date));
                }
            });

            result.Contacts?.ToList().ForEach(contactDto =>
            {
                if (source.UserProfile.Contacts.All(contact => contact != contactDto.Contact))
                {
                    result.Contacts.Remove(contactDto);
                }
            });

            source.UserProfile?.Contacts?.ForEach(contact =>
            {
                if (result.Contacts.All(contactDto => contactDto.Contact != contact))
                {
                    result.Contacts.Add(new ContactDto(contact));
                }
            });

            

            source.Invitations?.ForEach(i =>
            {
                if (result.Invitations.Any(iDto => iDto.Id == i.Id))
                {
                    var current = result.Invitations.First(iDto => iDto.Id == i.Id);
                    current.Status = i.Status;
                }
                else
                {
                    result.Invitations.Add(new InvitationDto(i.Id,
                        i.Status, i.GroupId, i.FromUser, i.ToUser, i.SuggestedRole));
                }
            });

            /*if (source.Notifies != null)
            {
                var notifiesDto = new List<NotifiesDto>();
                source.Notifies.ForEach(n => notifiesDto.Add(new NotifiesDto(n)));
                result.Notifies = notifiesDto;
            }*/
        }
    }
}
