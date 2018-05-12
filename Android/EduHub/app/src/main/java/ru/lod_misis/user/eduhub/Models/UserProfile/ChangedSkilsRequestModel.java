package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class ChangedSkilsRequestModel {
    @SerializedName("newSkills")
    @Expose
    private List<String> newSkills = null;

    public List<String> getNewSkills() {
        return newSkills;
    }

    public void setNewSkills(List<String> newSkills) {
        this.newSkills = newSkills;
    }
}
