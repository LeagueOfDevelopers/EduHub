package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ChangeSettingNotification {
    @SerializedName("configuringNotification")
    @Expose
    private String configuringNotification;
    @SerializedName("newValue")
    @Expose
    private String newValue;

    public String getConfiguringNotification() {
        return configuringNotification;
    }

    public void setConfiguringNotification(String configuringNotification) {
        this.configuringNotification = configuringNotification;
    }

    public String getNewValue() {
        return newValue;
    }

    public void setNewValue(String newValue) {
        this.newValue = newValue;
    }
}
