package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class NewMessageResponse {
    @SerializedName("Text")
    @Expose
    private String text;
    @SerializedName("GroupId")
    @Expose
    private Integer groupId;

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }

    public Integer getGroupId() {
        return groupId;
    }

    public void setGroupId(Integer groupId) {
        this.groupId = groupId;
    }
}
