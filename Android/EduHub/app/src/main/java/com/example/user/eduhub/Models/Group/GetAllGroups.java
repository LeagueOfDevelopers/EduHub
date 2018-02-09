package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 09.02.2018.
 */

public class GetAllGroups {
    @SerializedName("fillingGroups")
    @Expose
    private List<Group> fillingGroups = null;
    @SerializedName("fullGroups")
    @Expose
    private List<Group> fullGroups = null;

    public List<Group> getFillingGroups() {
        return fillingGroups;
    }

    public void setFillingGroups(List<Group> fillingGroups) {
        this.fillingGroups = fillingGroups;
    }

    public List<Group> getFullGroups() {
        return fullGroups;
    }

    public void setFullGroups(List<Group> fullGroups) {
        this.fullGroups = fullGroups;
    }
}
