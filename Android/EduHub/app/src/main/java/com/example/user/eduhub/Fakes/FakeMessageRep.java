package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.Message;
import com.example.user.eduhub.Interfaces.IMesasgeRepository;

import java.util.ArrayList;
import java.util.Date;

/**
 * Created by User on 23.12.2017.
 */

public class FakeMessageRep implements IMesasgeRepository {
    private ArrayList<Message> messages;
    @Override
    public ArrayList<Message> LoadMessages() {


        return messages;
    }

    @Override
    public boolean SaveNewMessage(Message newMessage) {
        messages.add(newMessage);
return  true;
    }
    public FakeMessageRep(){
        messages=new ArrayList<>();
        messages.add(new Message("Ярослав","Админ","Добро пожаловать в едухаб!",new Date()));
    }
}
