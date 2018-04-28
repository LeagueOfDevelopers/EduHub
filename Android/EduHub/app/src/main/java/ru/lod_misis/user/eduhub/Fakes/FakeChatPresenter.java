package ru.lod_misis.user.eduhub.Fakes;

import java.util.ArrayList;
import java.util.Date;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IChatPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IChatView;
import ru.lod_misis.user.eduhub.Models.Group.Message;

/**
 * Created by User on 20.04.2018.
 */

public class FakeChatPresenter implements IChatPresenter {
    IChatView chatView;
    ArrayList<Message> messages=new ArrayList<>();

    public FakeChatPresenter(IChatView chatView) {
        this.chatView = chatView;
    }

    @Override
    public void loadAllMessages(String token, String groupId) {
        Message message=new Message("Александр","1","Android Developer","2018-04-21T09:45:39.341Z","Это тестовое сообщение,чтобы сверстать простенький чатик",11);
        for (int i=0;i<10;i++){
            messages.add(message);
        }
        chatView.getMessages(messages);
    }

    @Override
    public void sendMessage(String token, String groupId, String messageText) {

    }


}
