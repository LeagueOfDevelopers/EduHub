package ru.lod_misis.user.eduhub.Interfaces.View;

import java.util.ArrayList;

import ru.lod_misis.user.eduhub.Models.Group.Message;
import ru.lod_misis.user.eduhub.Models.Group.NewMessageResponse;

/**
 * Created by User on 20.04.2018.
 */

public interface IChatView extends IBaseView {
    void getMessages(ArrayList<Message> messages);
    void getEmptyMessages();
    void newMessage(NewMessageResponse newMessageResponse);

}
