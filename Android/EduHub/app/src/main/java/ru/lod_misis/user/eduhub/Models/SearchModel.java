package ru.lod_misis.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 31.01.2018.
 */

public class SearchModel {
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("groupId")
    @Expose
    private String groupId;
    @SerializedName("username")
    @Expose
    private String username;

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
