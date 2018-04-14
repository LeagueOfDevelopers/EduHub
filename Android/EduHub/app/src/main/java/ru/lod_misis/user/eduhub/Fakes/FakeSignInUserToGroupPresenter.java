package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ISignInUserToGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ISignInUserToGroupView;

/**
 * Created by User on 02.02.2018.
 */

public class FakeSignInUserToGroupPresenter implements ISignInUserToGroupPresenter {
    ISignInUserToGroupView signInUserToGroupView;

    public FakeSignInUserToGroupPresenter(ISignInUserToGroupView signInUserToGroupView) {
        this.signInUserToGroupView = signInUserToGroupView;
    }

    @Override
    public void signInUserToGroup(String token, String groupId, Context context) {
        signInUserToGroupView.getResponse();
    }

    @Override
    public void signInTeacherToGroup(String token, String groupId, Context context) {
        signInUserToGroupView.getResponse();
    }
}
