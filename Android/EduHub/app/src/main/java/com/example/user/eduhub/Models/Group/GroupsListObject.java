package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 26.01.2018.
 */

public class GroupsListObject {
    @SerializedName("groups")
    @Expose
    private List<Group_> groups = null;

    public List<Group_> getGroups() {
        return groups;
    }

    public void setGroups(List<Group_> groups) {
        this.groups = groups;
    }

}

