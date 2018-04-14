package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 01.02.2018.
 */

public interface ISignInUserToGroupPresenter {
    void signInUserToGroup(String token,String groupId, Context context);
    void signInTeacherToGroup(String token,String groupId, Context context);
}
