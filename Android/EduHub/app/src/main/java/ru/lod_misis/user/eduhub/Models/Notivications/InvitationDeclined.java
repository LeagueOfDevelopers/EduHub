package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class InvitationDeclined {

    @SerializedName("GroupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("GroupId")
    @Expose
    private String groupId;
    @SerializedName("InvitedName")
    @Expose
    private String invitedName;
    @SerializedName("InvitedId")
    @Expose
    private String invitedId;

    public String getGroupTitle() {
        return groupTitle;
    }

    public void setGroupTitle(String groupTitle) {
        this.groupTitle = groupTitle;
    }

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }

    public String getInvitedName() {
        return invitedName;
    }

    public void setInvitedName(String invitedName) {
        this.invitedName = invitedName;
    }

    public String getInvitedId() {
        return invitedId;
    }

    public void setInvitedId(String invitedId) {
        this.invitedId = invitedId;
    }
}
