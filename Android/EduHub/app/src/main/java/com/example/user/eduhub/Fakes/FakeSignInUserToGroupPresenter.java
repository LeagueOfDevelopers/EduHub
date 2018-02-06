package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.Presenters.ISignInUserToGroupPresenter;
import com.example.user.eduhub.Interfaces.View.ISignInUserToGroupView;

/**
 * Created by User on 02.02.2018.
 */

public class FakeSignInUserToGroupPresenter implements ISignInUserToGroupPresenter {
    ISignInUserToGroupView signInUserToGroupView;

    public FakeSignInUserToGroupPresenter(ISignInUserToGroupView signInUserToGroupView) {
        this.signInUserToGroupView = signInUserToGroupView;
    }

    @Override
    public void signInUserToGroup(String token, String groupId) {
        signInUserToGroupView.getResponse();
    }
}