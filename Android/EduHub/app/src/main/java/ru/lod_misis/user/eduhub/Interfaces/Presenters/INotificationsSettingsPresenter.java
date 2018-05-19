package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import ru.lod_misis.user.eduhub.Models.UserProfile.ChangeSettingNotification;

public interface INotificationsSettingsPresenter {
    void getSettings(String token);
    void changeSettings(String token, ChangeSettingNotification model);
}
