package com.example.user.eduhub.Models.Group;

import com.example.user.eduhub.Classes.TypeOfEducation;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 * Created by User on 05.01.2018.
 */

public class GroupInfo implements Serializable {
    @SerializedName("id")
    @Expose
    private String id;
    @SerializedName("title")
    @Expose
    private String title;
    @SerializedName("memberAmount")
    @Expose
    private Integer memberAmount;
    @SerializedName("size")
    @Expose
    private Integer size;
    @SerializedName("cost")
    @Expose
    private Double cost;
    @SerializedName("groupType")
    @Expose
    private Integer groupType;
    @SerializedName("tags")
    @Expose

    private List<String> tags = null;
    @SerializedName("description")
    @Expose
    private String description;
    @SerializedName("currentAmount")
    @Expose
    private Integer currentAmount;
    @SerializedName("isPrivate")
    @Expose
    private Boolean isPrivate;
    @SerializedName("courseStatus")
    @Expose
    private Integer courseStatus;
    @SerializedName("curriculum")
    @Expose
    private String curriculum;
    @SerializedName("votersAmount")
    @Expose
    private Integer votersAmount;

    public Integer getVotersAmount() {
        return votersAmount;
    }

    public void setVotersAmount(Integer votersAmount) {
        this.votersAmount = votersAmount;
    }

    public String getCurriculum() {
        return curriculum;
    }

    public void setCurriculum(String curriculum) {
        this.curriculum = curriculum;
    }

    public Integer getCurrentAmount() {
        return currentAmount;
    }

    public void setCurrentAmount(Integer currentAmount) {
        this.currentAmount = currentAmount;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public Integer getMemberAmount() {
        return memberAmount;
    }

    public void setMemberAmount(Integer memberAmount) {
        this.memberAmount = memberAmount;
    }

    public Integer getSize() {
        return size;
    }

    public void setSize(Integer size) {
        this.size = size;
    }

    public Double getCost() {
        return cost;
    }

    public void setCost(Double cost) {
        this.cost = cost;
    }

    public Integer getGroupType() {
        return groupType;
    }

    public void setGroupType(Integer groupType) {
        this.groupType = groupType;
    }

    public List<String> getTags() {
        return tags;
    }

    public void setTags(List<String> tags) {
        this.tags = tags;
    }

    public Boolean getPrivate() {
        return isPrivate;
    }

    public void setPrivate(Boolean aPrivate) {
        isPrivate = aPrivate;
    }

    public Integer getCourseStatus() {
        return courseStatus;
    }

    public void setCourseStatus(Integer courseStatus) {
        this.courseStatus = courseStatus;
    }
}


