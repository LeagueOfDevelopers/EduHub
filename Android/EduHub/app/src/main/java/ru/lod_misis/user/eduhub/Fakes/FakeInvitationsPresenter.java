package ru.lod_misis.user.eduhub.Fakes;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.INotificationsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.INotificationsView;
import ru.lod_misis.user.eduhub.Models.Notivications.Invitation;

import java.util.ArrayList;

/**
 * Created by User on 02.02.2018.
 */

public class FakeInvitationsPresenter implements INotificationsPresenter {
    INotificationsView invitationsView;
    Invitation invitation=new Invitation();
    ArrayList<Invitation> invitations=new ArrayList<>();

    public FakeInvitationsPresenter(INotificationsView invitationsView) {
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

    @Override
    public void getAllNotifications(String token) {

    }
}
