package com.example.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 01.02.2018.
 */

public class Invitation {
    @SerializedName("status")
    @Expose
    private String status;
    @SerializedName("toGroup")
    @Expose
    private String toGroup;
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
    @SerializedName("fromUserName")
    @Expose
    private String fromUserName;
    @SerializedName("toUserName")
    @Expose
    private String toUserName;
    @SerializedName("toGroupTitle")
    @Expose
    private String toGroupTotle;

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getGroupId() {
        return toGroup;
    }

    public void setGroupId(String groupId) {
        this.toGroup = groupId;
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

    public String getFromUserName() {
        return fromUserName;
    }

    public void setFromUserName(String fromUserName) {
        this.fromUserName = fromUserName;
    }

    public String getToUserName() {
        return toUserName;
    }

    public void setToUserName(String toUserName) {
        this.toUserName = toUserName;
    }
}
