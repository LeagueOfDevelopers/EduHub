package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 04.02.2018.
 */

public class UserSearchProfileResponse {
    @SerializedName("users")
    @Expose
    private List<UserSearchProfile> users = null;

    public List<UserSearchProfile> getUsers() {
        return users;
    }

    public void setUsers(List<UserSearchProfile> users) {
        this.users = users;
    }
}

