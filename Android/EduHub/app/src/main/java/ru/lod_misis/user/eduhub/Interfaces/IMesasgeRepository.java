package ru.lod_misis.user.eduhub.Interfaces;

import ru.lod_misis.user.eduhub.Models.Group.Message;

import java.util.ArrayList;

/**
 * Created by User on 23.12.2017.
 */

public interface IMesasgeRepository {
    ArrayList<Message> LoadMessages();
    boolean SaveNewMessage(Message newMessage);
}
