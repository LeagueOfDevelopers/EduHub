using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Domain;
using System.Linq;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Extensions
{
    internal static class GroupExtensions
    {
        public static Group ParseFromGroupDto(GroupDto source)
        {
            var members = new List<Member>();
            source.Members.ToList().ForEach(user => members.Add(new Member(user.UserId,
                user.MemberRole, user.Paid, user.CurriculumStatus)));

            var tags = new List<string>();
            source.Tags.ToList().ForEach(tagDto => tags.Add(tagDto.Name));

            User teacher = null;
            if (source.TeacherId != 0)
            {
                teacher = new User(source.TeacherName, source.TeacherEmail, source.TeacherId);
            }


            var messages = new List<Message>();
            source.Messages.ForEach(message => messages.Add(new Message(message.Id, message.SenderId,
                message.SentOn, message.Text)));

            var invitations = new List<Invitation>();
            source.Invitations.ForEach(invitation => invitations.Add(new Invitation(invitation.FromUser, invitation.ToUser, 
                invitation.GroupId, invitation.SuggestedRole, invitation.Status, invitation.Id)));

            var groupInfo = new GroupInfo(source.Id, source.Title, source.Description,
                source.Curriculum, tags, source.GroupType,
                source.IsPrivate, source.IsActive, source.Size, source.Price);

            var result = new Group(messages, groupInfo, teacher, source.Status, members, invitations);
            return result;
        }

    }
}
