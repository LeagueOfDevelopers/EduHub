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

            sourse.GroupInfo.Tags.ToList().ForEach(tag => result.Tags.Add(new TagGroup(0, tag)));

            sourse.Members?.ForEach(member =>
                    result.Members.Add(new MemberDto(0, member.UserId, member.MemberRole,
                        member.Paid, member.CurriculumStatus)));

            sourse.Messages?.ToList().ForEach(message =>
                    result.Messages.Add(new MessageDto(message.Id, message.SenderId, message.SentOn, message.Text)));

            sourse.Invitations?.ForEach(i =>
            {
                if (result.Invitations.All(iDto => i.Id != iDto.Id))
                    result.Invitations.Add(new InvitationDto(i.Id,
                        i.Status, i.GroupId, i.FromUser, i.ToUser, i.SuggestedRole));
                result.Invitations.FirstOrDefault(iDto => iDto.Id == i.Id).Status = i.Status;
            });
        }
    }
}
