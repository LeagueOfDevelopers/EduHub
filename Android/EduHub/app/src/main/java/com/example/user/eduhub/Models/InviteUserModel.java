package com.example.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 31.01.2018.
 */

public class InviteUserModel {
    @SerializedName("invitedId")
    @Expose
    private String invitedId;
    @SerializedName("role")
    @Expose
    private String role;

    public String getInvitedId() {
        return invitedId;
    }

    public void setInvitedId(String invitedId) {
        this.invitedId = invitedId;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }
}
