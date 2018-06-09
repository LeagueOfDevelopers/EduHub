package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import ru.lod_misis.user.eduhub.Models.Notivications.Notification;
import ru.lod_misis.user.eduhub.Models.Notivications.Notifications;

public class GroupMessage {
    @SerializedName("notificationInfo")
    @Expose
    private Notifications notificationInfo;
    @SerializedName("id")
    @Expose
    private Integer id;
    @SerializedName("sentOn")
    @Expose
    private String sentOn;
    @SerializedName("messageType")
    @Expose
    private String messageType;

    public Notifications getNotificationInfo() {
        return notificationInfo;
    }

    public void setNotificationInfo(Notifications notificationInfo) {
        this.notificationInfo = notificationInfo;
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

    public String getMessageType() {
        return messageType;
    }

    public void setMessageType(String messageType) {
        this.messageType = messageType;
    }
}
