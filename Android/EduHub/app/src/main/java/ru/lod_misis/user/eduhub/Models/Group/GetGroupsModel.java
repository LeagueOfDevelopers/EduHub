package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by User on 09.01.2018.
 */

public class GetGroupsModel {

    @SerializedName("groups")
    @Expose
    private ArrayList<Group> groups = null;

    public ArrayList<Group> getGroups() {
        return groups;
    }

    public void setGroups(ArrayList<Group> groups) {
        this.groups = groups;
    }
}
