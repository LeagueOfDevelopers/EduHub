package com.example.user.eduhub.Models.UserProfile;

import com.example.user.eduhub.Models.Group.Group;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

/**
 * Created by User on 30.01.2018.
 */

public class TeacherProfile implements Serializable {
    @SerializedName("reviews")
    @Expose
    private List<Review> reviews = null;
    @SerializedName("skills")
    @Expose
    private List<String> skills = null;
    @SerializedName("jobExp")
    @Expose
    private List<Group> jobExp = null;

    public List<Review> getReviews() {
        return reviews;
    }

    public void setReviews(List<Review> reviews) {
        this.reviews = reviews;
    }

    public List<String> getSkills() {
        return skills;
    }

    public void setSkills(List<String> skills) {
        this.skills = skills;
    }

    public List<Group> getJobExp() {
        return jobExp;
    }

    public void setJobExp(List<Group> jobExp) {
        this.jobExp = jobExp;
    }
}
