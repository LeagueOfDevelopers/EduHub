package ru.lod_misis.user.eduhub.Interfaces.Presenters;

/**
 * Created by User on 01.02.2018.
 */

public interface INotificationsPresenter {
    void loadInvitations(String token);
    void getAllNotifications(String token);
}
