package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 16.01.2018.
 */

public interface IRegistrPresenter  {
    void RegistrationUser (String name,String email,String password,Boolean isTeacher, Context context);

}
