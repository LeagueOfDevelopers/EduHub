package com.example.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 31.01.2018.
 */

public class UserProfileResponse {
    @SerializedName("users")
    @Expose
    private List<UserProfile> users = null;

    public List<UserProfile> getUsers() {
        return users;
    }

    public void setUsers(List<UserProfile> users) {
        this.users = users;
    }

}
