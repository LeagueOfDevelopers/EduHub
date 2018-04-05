package ru.lod_misis.user.eduhub.Classes;

import java.util.Date;

/**
 * Created by User on 22.12.2017.
 */

public class Message {
    private String senderName;
    private String senderRole;
    private Date time;
    private String textMessage;
    private int id;
    public Message(String senderName,String senderRole,String textMessage,Date time){
        this.senderName=senderName;
        this.senderRole=senderRole;
        this.time=time;
        this.textMessage=textMessage;

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

}
