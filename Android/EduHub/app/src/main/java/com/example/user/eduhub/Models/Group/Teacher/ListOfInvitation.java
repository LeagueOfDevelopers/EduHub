package com.example.user.eduhub.Models.Group.Teacher;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 25.01.2018.
 */

public class ListOfInvitation {

    @SerializedName("status")
    @Expose
    private String status;
    @SerializedName("groupId")
    @Expose
    private String groupId;
    @SerializedName("fromUser")
    @Expose
    private String fromUser;
    @SerializedName("toUser")
    @Expose
    private String toUser;
    @SerializedName("suggestedRole")
    @Expose
    private String suggestedRole;
    @SerializedName("id")
    @Expose
    private String id;

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }

    public String getFromUser() {
        return fromUser;
    }

    public void setFromUser(String fromUser) {
        this.fromUser = fromUser;
    }

    public String getToUser() {
        return toUser;
    }

    public void setToUser(String toUser) {
        this.toUser = toUser;
    }

    public String getSuggestedRole() {
        return suggestedRole;
    }

    public void setSuggestedRole(String suggestedRole) {
        this.suggestedRole = suggestedRole;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

}