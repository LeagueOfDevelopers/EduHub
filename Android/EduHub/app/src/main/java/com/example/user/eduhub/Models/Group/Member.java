package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by User on 10.01.2018.
 */

public class Member implements Serializable{

    @SerializedName("userId")
    @Expose
    private String userId;
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("avatarLink")
    @Expose
    private String avatarLink;
    @SerializedName("memberRole")
    @Expose
    private int memberRole;
    @SerializedName("paid")
    @Expose
    private Boolean paid;
    @SerializedName("acceptedCourse")
    @Expose
    private Boolean acceptedCourse;

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAvatarLink() {
        return avatarLink;
    }

    public void setAvatarLink(String avatarLink) {
        this.avatarLink = avatarLink;
    }

    public int getMemberRole() {
        return memberRole;
    }

    public void setMemberRole(int memberRole) {
        this.memberRole = memberRole;
    }

    public Boolean getPaid() {
        return paid;
    }

    public void setPaid(Boolean paid) {
        this.paid = paid;
    }

    public Boolean getAcceptedCourse() {
        return acceptedCourse;
    }

    public void setAcceptedCourse(Boolean acceptedCourse) {
        this.acceptedCourse = acceptedCourse;
    }
}