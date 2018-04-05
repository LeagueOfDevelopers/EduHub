package ru.lod_misis.user.eduhub.Fakes;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IInviteUserPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IInviteUserView;

/**
 * Created by User on 02.02.2018.
 */

public class FakeInviteUserPresenter  implements IInviteUserPresenter {
    IInviteUserView inviteUserView;

    public FakeInviteUserPresenter(IInviteUserView inviteUserView) {
        this.inviteUserView = inviteUserView;
    }

    @Override
    public void inviteUser(String name, String role,String groupId,String myId) {
        inviteUserView.getResponse();
    }
}
