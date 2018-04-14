package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 01.02.2018.
 */

public interface INotificationsPresenter {
    void loadInvitations(String token, Context context);
    void getAllNotifications(String token, Context context);
}
