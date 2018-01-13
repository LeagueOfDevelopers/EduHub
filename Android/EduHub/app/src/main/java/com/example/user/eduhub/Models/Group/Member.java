package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 10.01.2018.
 */

public class Member {


    @SerializedName("userId")
    @Expose
    private String userId;
    @SerializedName("memberRole")
    @Expose
    private Integer memberRole;
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

    public Integer getMemberRole() {
        return memberRole;
    }

    public void setMemberRole(Integer memberRole) {
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