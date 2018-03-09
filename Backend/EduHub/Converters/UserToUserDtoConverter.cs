using System.Collections.Generic;
using AutoMapper;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;

namespace EduHub.Converters
{
    public class UserToUserDtoConverter : ITypeConverter<User, UserDto>
    {
        public UserDto Convert(User source, UserDto destination, ResolutionContext context)
        {
            var result = new UserDto();
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
            var listReviews = new List<ReviewDto>(); 
            source.TeacherProfile.Reviews.ForEach(r => listReviews.Add(new ReviewDto(0, r.FromUser,
                r.FromGroup, r.Title, r.Text, r.Date)));
            result.Reviews = listReviews;
            if (source.TeacherProfile.Skills != null)
            {
                var listSkills = new List<SkillsDto>();
                source.TeacherProfile.Skills.ForEach(s => listSkills.Add(new SkillsDto(0, s)));
                result.Skills = listSkills;
            }

            if (source.UserProfile.Contacts != null)
            {
                var contactDto = new List<ContactDto>();
                source.UserProfile.Contacts.ForEach(c => contactDto.Add(new ContactDto(0, c)));
                result.Contacts = contactDto;
            }

            if (source.Invitations != null)
            {
                var invitationDto = new List<InvitationDto>();
                source.Invitations.ForEach(i => invitationDto.Add(new InvitationDto(0, i.Status,
                    i.GroupId, i.FromUser, i.ToUser, i.SuggestedRole)));
                result.Invitations = invitationDto;
            }

            if (source.Notifies != null)
            {
                var notifiesDto = new List<NotifiesDto>();
                source.Notifies.ForEach(n => new NotifiesDto(0, n));
                result.Notifies = notifiesDto;
            }

            return result;
        }
    }
}
