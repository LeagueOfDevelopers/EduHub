package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Interfaces.Presenters.IInviteUserPresenter;
import com.example.user.eduhub.Interfaces.View.IInviteUserView;

/**
 * Created by User on 02.02.2018.
 */

public class FakeInviteUserPresenter  implements IInviteUserPresenter {
    IInviteUserView inviteUserView;

    public FakeInviteUserPresenter(IInviteUserView inviteUserView) {
        this.inviteUserView = inviteUserView;
    }

    @Override
    public void inviteUser(String name, MemberRole role,String groupId,String myId) {
        inviteUserView.getResponse();
    }
}
