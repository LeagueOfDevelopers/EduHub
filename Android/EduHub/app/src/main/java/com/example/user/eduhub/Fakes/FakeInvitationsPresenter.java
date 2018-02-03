package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.Presenters.IInvitationsPresenter;
import com.example.user.eduhub.Interfaces.View.IInvitationsView;
import com.example.user.eduhub.Models.Invitation;

import java.util.ArrayList;

/**
 * Created by User on 02.02.2018.
 */

public class FakeInvitationsPresenter implements IInvitationsPresenter {
    IInvitationsView invitationsView;
    Invitation invitation=new Invitation();
    ArrayList<Invitation> invitations=new ArrayList<>();

    public FakeInvitationsPresenter(IInvitationsView invitationsView) {
        this.invitationsView = invitationsView;
    }

    @Override
    public void loadInvitations(String token) {
        invitation.setFromUser("01550cbd-8005-4b67-b9aa-691c7a85c499");
        invitation.setGroupId("01550cbd-8005-4b67-b9aa-691c7a85c499");
        invitation.setId("01550cbd-8005-4b67-b9aa-691c7a85c499");
        invitation.setStatus("Unknown");
        invitation.setSuggestedRole(1+"");
        invitation.setFromUserName("Ярослав");
        invitation.setToUserName("Александр");
        invitation.setToUser("01550cbd-8005-4b67-b9aa-691c7a85c499");
        for(int i=0;i<10;i++){
            invitations.add(invitation);
        }
        invitationsView.getInvitations(invitations);
        invitationsView.stopLoading();

    }
}
