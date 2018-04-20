using AutoMapper;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Mailing.MessageModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace EduHubLibrary.Mailing
{
    public class MessageMapper
    {
        public object MapNotification(INotificationInfo notification, string receiverName)
        {
            return Map((dynamic)notification, receiverName);
        }

        private CourseFinishedMessage Map(CourseFinishedNotification notification, string receiverName)
        {
            return new CourseFinishedMessage(notification.GroupTitle, notification.TeacherName, receiverName);
        }

        private CurriculumAcceptedMessage Map(CurriculumAcceptedNotification notification, string receiverName)
        {
            return new CurriculumAcceptedMessage(notification.GroupTitle, receiverName);
        }

        private CurriculumDeclinedMessage Map(CurriculumDeclinedNotification notification, string receiverName)
        {
            return new CurriculumDeclinedMessage(notification.GroupTitle, notification.DeclinedName, receiverName);
        }

        private CurriculumSuggestedMessage Map(CurriculumSuggestedNotification notification, string receiverName)
        {
            return new CurriculumSuggestedMessage(notification.CurriculumLink, notification.GroupTitle, receiverName);
        }

        private GroupIsFormedMessage Map(GroupIsFormedNotification notification, string receiverName)
        {
            return new GroupIsFormedMessage(notification.GroupTitle, receiverName);
        }

        private InvitationAcceptedMessage Map(InvitationAcceptedNotification notification, string receiverName)
        {
            return new InvitationAcceptedMessage(notification.GroupTitle, notification.InvitedName, receiverName);
        }

        private InvitationDeclinedMessage Map(InvitationDeclinedNotification notification, string receiverName)
        {
            return new InvitationDeclinedMessage(notification.GroupTitle, notification.InvitedName, receiverName);
        }

        private InvitationReceivedMessage Map(InvitationReceivedNotification notification, string receiverName)
        {
            return new InvitationReceivedMessage(notification.GroupTitle, notification.InviterName, notification.SuggestedRole, receiverName);
        }

        private MemberLeftMessage Map(MemberLeftNotification notification, string receiverName)
        {
            return new MemberLeftMessage(notification.GroupTitle, notification.Username, receiverName);
        }

        private NewCreatorMessage Map(NewCreatorNotification notification, string receiverName)
        {
            return new NewCreatorMessage(notification.GroupTitle, notification.ExCreatorUsername, notification.NewCreatorUsername, receiverName);
        }

        private NewMemberMessage Map(NewMemberNotification notification, string receiverName)
        {
            return new NewMemberMessage(notification.GroupTitle, notification.Username, receiverName);
        }

        private ReportMessage Map(ReportMessageNotification notification, string receiverName)
        {
            return new ReportMessage(notification.SenderName, notification.SuspectedName, notification.Reason, notification.Description, receiverName);
        }

        private ReviewReceivedMessage Map(ReviewReceivedNotification notification, string receiverName)
        {
            return new ReviewReceivedMessage(notification.GroupTitle, notification.ReviewerName, notification.ReviewType, receiverName);
        }

        private SanctionsAppliedToAdminMessage Map(SanctionAppliedToAdminNotification notification, string receiverName)
        {
            return new SanctionsAppliedToAdminMessage(notification.BrokenRule, notification.SanctionType, notification.Username, receiverName);
        }

        private SanctionsAppliedToUserMessage Map(SanctionsAppliedToUserNotification notification, string receiverName)
        {
            return new SanctionsAppliedToUserMessage(notification.BrokenRule, notification.SanctionType, receiverName);
        }

        private TeacherFoundMessage Map(TeacherFoundNotification notification, string receiverName)
        {
            return new TeacherFoundMessage(notification.TeacherName, notification.GroupTitle, receiverName);
        }
    }
}
