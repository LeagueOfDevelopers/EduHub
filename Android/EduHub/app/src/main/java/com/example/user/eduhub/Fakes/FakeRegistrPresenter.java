package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Fragments.LoginFragment;
import com.example.user.eduhub.Interfaces.Presenters.IRegistrPresenter;
import com.example.user.eduhub.Interfaces.View.IRegistrView;

/**
 * Created by User on 18.01.2018.
 */

public class FakeRegistrPresenter implements IRegistrPresenter {
    private IRegistrView registrView;

    public FakeRegistrPresenter(IRegistrView registrView) {
        this.registrView = registrView;
    }

    @Override
    public void RegistrationUser(String name, String email, String password, Boolean isTeacher, String avatarLink, String inviteCode) {
        LoginFragment fragment=new LoginFragment();
        registrView.getResponse(fragment);
    }
}
