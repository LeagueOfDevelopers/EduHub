using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Data.Connections;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Extensions
{
    public static class GroupDtoExtensions
    {
        public static void ParseFromGroup(this GroupDto result, Group sourse)
        {
            result.Id = sourse.GroupInfo.Id;
            result.Title = sourse.GroupInfo.Title;
            result.Description = sourse.GroupInfo.Description;
            result.Curriculum = sourse.GroupInfo.Curriculum;
            result.IsPrivate = sourse.GroupInfo.IsPrivate;
            result.IsActive = sourse.GroupInfo.IsActive;
            result.Size = sourse.GroupInfo.Size;
            result.Price = sourse.GroupInfo.Price;
            result.Status = sourse.Status;
            result.GroupType = sourse.GroupInfo.GroupType;

            if(sourse.Teacher != null)
            { 
                result.TeacherId = sourse.Teacher.Id;
                result.TeacherEmail = sourse.Teacher.UserProfile.Email;
                result.TeacherName = sourse.Teacher.UserProfile.Name;
            }

            /*sourse.GroupInfo.Tags.ToList().ForEach(tag =>
            {
                if (result.Tags.All(tagDto => tagDto.Name != tag))
                {
                    result.GroupTags.Add(new GroupTag(tag, new TagDto(tag), result.Id, result));
                }
            });*/

            sourse.Members?.ForEach(member =>
            {
                if (result.Members.All(memberDto => memberDto.UserId != member.UserId))
                {
                    result.Members.Add(new MemberDto(0, member.UserId, member.MemberRole,
                        member.Paid, member.CurriculumStatus));
                }

                result.Members.First(memberDto => memberDto.UserId == member.UserId).CurriculumStatus =
                    member.CurriculumStatus;
            });

            sourse.Messages?.ToList().ForEach(message =>
            {
                if (result.Messages.All(messageDto => messageDto.Id != message.Id))
                {
                    result.Messages.Add(new MessageDto(message.Id, message.SenderId, message.SentOn, message.Text));
                }
            });

            sourse.Invitations?.ToList().ForEach(invitation =>
            {
                if (result.Invitations.All(invitationDto => invitationDto.Id != invitation.Id))
                {
                    result.Invitations.Add(new InvitationDto(invitation.Id, invitation.Status, invitation.GroupId, invitation.FromUser, invitation.ToUser, invitation.SuggestedRole));
                }
            });
        }
    }
}
