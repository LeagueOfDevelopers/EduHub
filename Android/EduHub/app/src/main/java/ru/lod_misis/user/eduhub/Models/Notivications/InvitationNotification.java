package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class InvitationNotification {
    @SerializedName("GroupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("InviterName")
    @Expose
    private String inviterName;
    @SerializedName("InvitedId")
    @Expose
    private Integer invitedId;
    @SerializedName("SuggestedRole")
    @Expose
    private Integer suggestedRole;

    public String getGroupTitle() {
        return groupTitle;
    }

    public void setGroupTitle(String groupTitle) {
        this.groupTitle = groupTitle;
    }

    public String getInviterName() {
        return inviterName;
    }

    public void setInviterName(String inviterName) {
        this.inviterName = inviterName;
    }

    public Integer getInvitedId() {
        return invitedId;
    }

    public void setInvitedId(Integer invitedId) {
        this.invitedId = invitedId;
    }

    public Integer getSuggestedRole() {
        return suggestedRole;
    }

    public void setSuggestedRole(Integer suggestedRole) {
        this.suggestedRole = suggestedRole;
    }

}
