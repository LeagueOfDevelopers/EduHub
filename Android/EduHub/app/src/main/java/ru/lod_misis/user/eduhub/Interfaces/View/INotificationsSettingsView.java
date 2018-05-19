package ru.lod_misis.user.eduhub.Interfaces.View;

import ru.lod_misis.user.eduhub.Models.UserProfile.NotificationsSettings;

public interface INotificationsSettingsView extends IBaseView {
    void getSettings(NotificationsSettings settings);
}
