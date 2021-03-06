package ru.lod_misis.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

import ru.lod_misis.user.eduhub.Models.Notivications.Invitation;

/**
 * Created by User on 01.02.2018.
 */

public class InvitationResponse {
    @SerializedName("invitations")
    @Expose
    private List<Invitation> invitations = null;

    public List<Invitation> getInvitations() {
        return invitations;
    }

    public void setInvitations(List<Invitation> invitations) {
        this.invitations = invitations;
    }
}
