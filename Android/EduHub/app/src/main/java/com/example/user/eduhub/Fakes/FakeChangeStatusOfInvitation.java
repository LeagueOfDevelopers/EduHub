package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.Presenters.IChangeStatusOfInvitationPresenter;
import com.example.user.eduhub.Interfaces.View.IChangeStatusOfInvitationView;

/**
 * Created by User on 02.02.2018.
 */

public class FakeChangeStatusOfInvitation implements IChangeStatusOfInvitationPresenter {
    IChangeStatusOfInvitationView changeStatusOfInvitationView;

    public FakeChangeStatusOfInvitation(IChangeStatusOfInvitationView changeStatusOfInvitationView) {
        this.changeStatusOfInvitationView = changeStatusOfInvitationView;
    }

    @Override
    public void changeStatus(String newStatus, String token, String inviteId) {
        if(newStatus.equals("Accepted")){
            changeStatusOfInvitationView.Possitive();
        }
        else {
            changeStatusOfInvitationView.Negative();
        }

    }
}
