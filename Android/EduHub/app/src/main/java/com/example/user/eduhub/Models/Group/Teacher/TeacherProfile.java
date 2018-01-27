package com.example.user.eduhub.Models.Group.Teacher;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 25.01.2018.
 */

public class TeacherProfile {

    @SerializedName("reviews")
    @Expose
    private List<Review> reviews = null;
    @SerializedName("skills")
    @Expose
    private List<String> skills = null;

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

}