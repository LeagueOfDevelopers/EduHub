package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by User on 22.12.2017.
 */

public class Message {
    @SerializedName("senderName")
    @Expose
    private String senderName;
    @SerializedName("senderId")
    @Expose
    private String senderId;
    @SerializedName("senderRole")
    @Expose
    private String senderRole;
    @SerializedName("sentOn")
    @Expose
    private String time;
    @SerializedName("text")
    @Expose
    private String textMessage;
    @SerializedName("id")
    @Expose
    private int id;



    public Message(String senderName, String senderId, String senderRole, String time, String textMessage, int id) {

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
    public String getTime(){
        return time;
    }
    public String getSenderId() {
        return senderId;
    }

    public int getId() {
        return id;
    }

}
