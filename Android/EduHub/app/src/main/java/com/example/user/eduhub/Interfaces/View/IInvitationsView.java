package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.Invitation;

import java.util.ArrayList;

/**
 * Created by User on 01.02.2018.
 */

public interface IInvitationsView extends IBaseView {
    void getInvitations(ArrayList<Invitation> invitations);
}
