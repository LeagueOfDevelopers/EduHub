package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class NotificationsSettings {
    @SerializedName("Default")
    @Expose
    private String _default;
    @SerializedName("CourseFinished")
    @Expose
    private String courseFinished;
    @SerializedName("CurriculumAccepted")
    @Expose
    private String curriculumAccepted;
    @SerializedName("CurriculumDeclined")
    @Expose
    private String curriculumDeclined;
    @SerializedName("CurriculumSuggested")
    @Expose
    private String curriculumSuggested;
    @SerializedName("GroupIsFormed")
    @Expose
    private String groupIsFormed;
    @SerializedName("InvitationAccepted")
    @Expose
    private String invitationAccepted;
    @SerializedName("InvitationDeclined")
    @Expose
    private String invitationDeclined;
    @SerializedName("InvitationReceived")
    @Expose
    private String invitationReceived;
    @SerializedName("MemberLeft")
    @Expose
    private String memberLeft;
    @SerializedName("NewCreator")
    @Expose
    private String newCreator;
    @SerializedName("NewMember")
    @Expose
    private String newMember;
    @SerializedName("ReportMessage")
    @Expose
    private String reportMessage;
    @SerializedName("ReviewReceived")
    @Expose
    private String reviewReceived;
    @SerializedName("SanctionsAppliedToUser")
    @Expose
    private String sanctionsAppliedToUser;
    @SerializedName("SanctionsAppliedToAdmin")
    @Expose
    private String sanctionsAppliedToAdmin;
    @SerializedName("TeacherFound")
    @Expose
    private String teacherFound;
    @SerializedName("SanctionsCancelledToUser")
    @Expose
    private String sanctionsCancelledToUser;
    @SerializedName("SanctionsCancelledToAdmin")
    @Expose
    private String sanctionsCancelledToAdmin;

    public String getDefault() {
        return _default;
    }

    public void setDefault(String _default) {
        this._default = _default;
    }

    public String getCourseFinished() {
        return courseFinished;
    }

    public void setCourseFinished(String courseFinished) {
        this.courseFinished = courseFinished;
    }

    public String getCurriculumAccepted() {
        return curriculumAccepted;
    }

    public void setCurriculumAccepted(String curriculumAccepted) {
        this.curriculumAccepted = curriculumAccepted;
    }

    public String getCurriculumDeclined() {
        return curriculumDeclined;
    }

    public void setCurriculumDeclined(String curriculumDeclined) {
        this.curriculumDeclined = curriculumDeclined;
    }

    public String getCurriculumSuggested() {
        return curriculumSuggested;
    }

    public void setCurriculumSuggested(String curriculumSuggested) {
        this.curriculumSuggested = curriculumSuggested;
    }

    public String getGroupIsFormed() {
        return groupIsFormed;
    }

    public void setGroupIsFormed(String groupIsFormed) {
        this.groupIsFormed = groupIsFormed;
    }

    public String getInvitationAccepted() {
        return invitationAccepted;
    }

    public void setInvitationAccepted(String invitationAccepted) {
        this.invitationAccepted = invitationAccepted;
    }

    public String getInvitationDeclined() {
        return invitationDeclined;
    }

    public void setInvitationDeclined(String invitationDeclined) {
        this.invitationDeclined = invitationDeclined;
    }

    public String getInvitationReceived() {
        return invitationReceived;
    }

    public void setInvitationReceived(String invitationReceived) {
        this.invitationReceived = invitationReceived;
    }

    public String getMemberLeft() {
        return memberLeft;
    }

    public void setMemberLeft(String memberLeft) {
        this.memberLeft = memberLeft;
    }

    public String getNewCreator() {
        return newCreator;
    }

    public void setNewCreator(String newCreator) {
        this.newCreator = newCreator;
    }

    public String getNewMember() {
        return newMember;
    }

    public void setNewMember(String newMember) {
        this.newMember = newMember;
    }

    public String getReportMessage() {
        return reportMessage;
    }

    public void setReportMessage(String reportMessage) {
        this.reportMessage = reportMessage;
    }

    public String getReviewReceived() {
        return reviewReceived;
    }

    public void setReviewReceived(String reviewReceived) {
        this.reviewReceived = reviewReceived;
    }

    public String getSanctionsAppliedToUser() {
        return sanctionsAppliedToUser;
    }

    public void setSanctionsAppliedToUser(String sanctionsAppliedToUser) {
        this.sanctionsAppliedToUser = sanctionsAppliedToUser;
    }

    public String getSanctionsAppliedToAdmin() {
        return sanctionsAppliedToAdmin;
    }

    public void setSanctionsAppliedToAdmin(String sanctionsAppliedToAdmin) {
        this.sanctionsAppliedToAdmin = sanctionsAppliedToAdmin;
    }

    public String getTeacherFound() {
        return teacherFound;
    }

    public void setTeacherFound(String teacherFound) {
        this.teacherFound = teacherFound;
    }

    public String getSanctionsCancelledToUser() {
        return sanctionsCancelledToUser;
    }

    public void setSanctionsCancelledToUser(String sanctionsCancelledToUser) {
        this.sanctionsCancelledToUser = sanctionsCancelledToUser;
    }

    public String getSanctionsCancelledToAdmin() {
        return sanctionsCancelledToAdmin;
    }

    public void setSanctionsCancelledToAdmin(String sanctionsCancelledToAdmin) {
        this.sanctionsCancelledToAdmin = sanctionsCancelledToAdmin;
    }

}
