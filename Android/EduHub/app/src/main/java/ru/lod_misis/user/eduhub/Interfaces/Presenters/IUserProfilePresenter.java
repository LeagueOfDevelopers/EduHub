package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 30.01.2018.
 */

public interface IUserProfilePresenter  {
    void loadUserProfile(String token,String userId, Context context);
}
