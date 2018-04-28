package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class NewMessage {

    @SerializedName("text")
    @Expose
    private String text;

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }
}
