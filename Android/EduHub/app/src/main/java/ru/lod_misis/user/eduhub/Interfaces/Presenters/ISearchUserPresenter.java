package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 31.01.2018.
 */

public interface ISearchUserPresenter {

    void searchUser(String name, Context context);
    void searchUserForInvitation(String name,String groupId, Context context);
}
