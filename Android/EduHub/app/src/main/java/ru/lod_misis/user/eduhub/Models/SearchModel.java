package ru.lod_misis.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 31.01.2018.
 */

public class SearchModel {
    @SerializedName("groupId")
    @Expose
    private String groupId;
    @SerializedName("username")
    @Expose
    private String username;
    @SerializedName("wantToTeach")
    @Expose
    private Boolean wantToTeach;

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

    public Boolean getWantToTeach() {
        return wantToTeach;
    }

    public void setWantToTeach(Boolean wantToTeach) {
        this.wantToTeach = wantToTeach;
    }
}
