using System.Collections.Generic;
using System.Linq;
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

            source.TeacherProfile?.Skills?.ForEach(s => new TagUser(0, s));

            source.TeacherProfile?.Reviews?.ForEach(review => 
                result.Reviews.Add(new ReviewDto(review.Id, review.FromUser, review.FromGroup,
                review.Title, review.Text, review.Date)));

            source.UserProfile?.Contacts?.ForEach(contact => 
                result.Contacts.Add(new ContactDto(0, contact)));

            source.Invitations?.ForEach(i =>
            {
                if(result.Invitations.All(iDto => i.Id != iDto.Id))
                    result.Invitations.Add(new InvitationDto(i.Id,
                    i.Status, i.GroupId, i.FromUser, i.ToUser, i.SuggestedRole));
                result.Invitations.FirstOrDefault(iDto => iDto.Id == i.Id).Status = i.Status;
            });

            source.Notifies?.ToList().ForEach(n => result.Notifies.Add(new NotifiesDto(0, n)));
        }
    }
}
