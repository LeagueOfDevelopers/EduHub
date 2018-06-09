package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by User on 22.12.2017.
 */

public class Message {
    @SerializedName("senderId")
    @Expose
    private Integer senderId;
    @SerializedName("senderName")
    @Expose
    private String senderName;
    @SerializedName("text")
    @Expose
    private String text;
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("sentOn")
    @Expose
    private String sentOn;
    @SerializedName("messageType")
    @Expose
    private Integer messageType;
    @SerializedName("notificationInfo")
    @Expose
    private String notificationInfo;
    @SerializedName("notificationType")
    @Expose
    private Integer notificationType;

    public Integer getSenderId() {
        return senderId;
    }

    public void setSenderId(Integer senderId) {
        this.senderId = senderId;
    }

    public String getSenderName() {
        return senderName;
    }

    public void setSenderName(String senderName) {
        this.senderName = senderName;
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getSentOn() {
        return sentOn;
    }

    public void setSentOn(String sentOn) {
        this.sentOn = sentOn;
    }

    public Integer getMessageType() {
        return messageType;
    }

    public void setMessageType(Integer messageType) {
        this.messageType = messageType;
    }

    public String getNotificationInfo() {
        return notificationInfo;
    }

    public void setNotificationInfo(String notificationInfo) {
        this.notificationInfo = notificationInfo;
    }

    public Integer getNotificationType() {
        return notificationType;
    }

    public void setNotificationType(Integer notificationType) {
        this.notificationType = notificationType;
    }


}
