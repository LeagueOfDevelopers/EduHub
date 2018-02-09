package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

/**
 * Created by User on 09.02.2018.
 */

public class Group implements Serializable{
    @SerializedName("members")
    @Expose
    private List<Member> members;
    @SerializedName("groupInfo")
    @Expose
    private GroupInfo groupInfo;
    @SerializedName("status")
    @Expose
    private String status;
    @SerializedName("educator")
    @Expose
    private Educator educator;
    @SerializedName("numberOfMembers")
    @Expose
    private Integer numberOfMembers;
    @SerializedName("chat")
    @Expose
    private Chat chat;

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

    public Integer getNumberOfMembers() {
        return numberOfMembers;
    }

    public void setNumberOfMembers(Integer numberOfMembers) {
        this.numberOfMembers = numberOfMembers;
    }

    public Educator getEducator() {
        return educator;
    }

    public void setEducator(Educator educator) {
        this.educator = educator;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }
}
