package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

/**
 * Created by User on 09.01.2018.
 */

public class Group implements Serializable {

    @SerializedName("members")
    @Expose
    private List<Member> members = null;
    @SerializedName("groupInfo")
    @Expose
    private GroupInfo groupInfo;
    @SerializedName("course")
    @Expose
    private Object course;
    @SerializedName("teacher")
    @Expose
    private Object teacher;

    public List<Member> getMembers() {
        return members;
    }

    public void setMembers(List<Member> members) {
        this.members = members;
    }

    public GroupInfo getGroupInfo() {
        return groupInfo;
    }

    public void setGroupInfo(GroupInfo groupInfo) {
        this.groupInfo = groupInfo;
    }

    public Object getCourse() {
        return course;
    }

    public void setCourse(Object course) {
        this.course = course;
    }

    public Object getTeacher() {
        return teacher;
    }

    public void setTeacher(Object teacher) {
        this.teacher = teacher;
    }
}
