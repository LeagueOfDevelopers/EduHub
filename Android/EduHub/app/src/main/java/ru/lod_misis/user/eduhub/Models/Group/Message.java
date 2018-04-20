package ru.lod_misis.user.eduhub.Models.Group;

import java.util.Date;

/**
 * Created by User on 22.12.2017.
 */

public class Message {

    private String senderName;
    private String senderId;
    private String senderRole;
    private Date time;
    private String textMessage;
    private int id;



    public Message(String senderName, String senderId, String senderRole, Date time, String textMessage, int id) {

        this.senderName = senderName;
        this.senderId = senderId;
        this.senderRole = senderRole;
        this.time = time;
        this.textMessage = textMessage;
        this.id = id;
    }

    public String getSenderName(){
        return senderName;
    }
    public String getSenderRole(){
        return senderRole;
    }
    public String getTextMessage(){
        return textMessage;
    }
    public Date getTime(){
        return time;
    }
    public String getSenderId() {
        return senderId;
    }

    public int getId() {
        return id;
    }

}
