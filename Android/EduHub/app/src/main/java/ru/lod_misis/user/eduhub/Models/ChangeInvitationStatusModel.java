package ru.lod_misis.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 02.02.2018.
 */

public class ChangeInvitationStatusModel {
    @SerializedName("invitationId")
    @Expose
    private String invitationId;
    @SerializedName("status")
    @Expose
    private String status;

    public String getInvitationId() {
        return invitationId;
    }

    public void setInvitationId(String invitationId) {
        this.invitationId = invitationId;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
