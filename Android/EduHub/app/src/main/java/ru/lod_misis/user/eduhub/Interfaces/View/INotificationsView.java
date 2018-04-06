package ru.lod_misis.user.eduhub.Interfaces.View;

import ru.lod_misis.user.eduhub.Models.Notivications.Invitation;
import ru.lod_misis.user.eduhub.Models.Notivications.Notification;

import java.util.ArrayList;

/**
 * Created by User on 01.02.2018.
 */

public interface INotificationsView extends IBaseView {
    void getInvitations(ArrayList<Invitation> invitations);
    void getAllNotifications(ArrayList<Notification> notifications);
}
