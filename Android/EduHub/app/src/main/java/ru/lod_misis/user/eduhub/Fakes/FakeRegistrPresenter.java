package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;

import ru.lod_misis.user.eduhub.Fragments.LoginFragment;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IRegistrPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IRegistrView;

/**
 * Created by User on 18.01.2018.
 */

public class FakeRegistrPresenter implements IRegistrPresenter {
    private IRegistrView registrView;

    public FakeRegistrPresenter(IRegistrView registrView) {
        this.registrView = registrView;
    }

    @Override
    public void RegistrationUser(String name, String email, String password, Boolean isTeacher, Context context) {
        LoginFragment fragment=new LoginFragment();
        registrView.getResponse(fragment);
    }
}
