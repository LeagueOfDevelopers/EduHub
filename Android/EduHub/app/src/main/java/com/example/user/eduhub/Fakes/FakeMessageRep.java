package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.Message;
import com.example.user.eduhub.Interfaces.IMesasgeRepository;

import java.util.ArrayList;
import java.util.Date;

/**
 * Created by User on 23.12.2017.
 */

public class FakeMessageRep implements IMesasgeRepository {
    @Override
    public ArrayList<Message> LoadMessages() {
        ArrayList<Message> messages=new ArrayList<>();
        messages.add(new Message("Ярослав","Админ","Добро пожаловать в едухаб!",new Date()));

        return messages;
    }

    @Override
    public boolean SaveNewMessage(Message newMessage) {
return  true;
    }
}
