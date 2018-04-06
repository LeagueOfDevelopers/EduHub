package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class ReportMessage {
    @SerializedName("SenderName")
    @Expose
    private String senderName;
    @SerializedName("SenderId")
    @Expose
    private String senderId;
    @SerializedName("SuspectedName")
    @Expose
    private String suspectedName;
    @SerializedName("SuspectedId")
    @Expose
    private String suspectedId;
    @SerializedName("BrokenRule")
    @Expose
    private String brokenRule;

    public String getSenderName() {
        return senderName;
    }

    public void setSenderName(String senderName) {
        this.senderName = senderName;
    }

    public String getSenderId() {
        return senderId;
    }

    public void setSenderId(String senderId) {
        this.senderId = senderId;
    }

    public String getSuspectedName() {
        return suspectedName;
    }

    public void setSuspectedName(String suspectedName) {
        this.suspectedName = suspectedName;
    }

    public String getSuspectedId() {
        return suspectedId;
    }

    public void setSuspectedId(String suspectedId) {
        this.suspectedId = suspectedId;
    }

    public String getBrokenRule() {
        return brokenRule;
    }

    public void setBrokenRule(String brokenRule) {
        this.brokenRule = brokenRule;
    }
}
