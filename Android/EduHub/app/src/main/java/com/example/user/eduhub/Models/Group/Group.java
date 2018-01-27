package com.example.user.eduhub.Models.Group;

import com.example.user.eduhub.Models.Group.Teacher.Teacher;
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
    private List<Member_> members;
    @SerializedName("groupInfo")
    @Expose
    private GroupInfo groupInfo;
    @SerializedName("course")
    @Expose
    private Object course;
    @SerializedName("numberOfMembers")
    @Expose
    private Integer numberOfMembers;
    @SerializedName("teacher")
    @Expose
    private Teacher teacher;
    @SerializedName("chat")
    @Expose
    private Chat chat;

    public List<Member_> getMembers() {
        return members;
    }

    public void setMembers(List<Member_> members) {
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

    public Teacher getTeacher() {
        return teacher;
    }

    public void setTeacher(Teacher teacher) {
        this.teacher = teacher;
    }

    public Integer getNumberOfMembers() {
        return numberOfMembers;
    }

    public void setNumberOfMembers(Integer numberOfMembers) {
        this.numberOfMembers = numberOfMembers;
    }
}
