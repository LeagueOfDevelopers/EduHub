package ru.lod_misis.user.eduhub.Interfaces.Presenters;

/**
 * Created by User on 20.04.2018.
 */

public interface IChatPresenter {
    void loadAllMessages(String token,String groupId);
    void sendMessage(String token,String groupId,String text);
}
