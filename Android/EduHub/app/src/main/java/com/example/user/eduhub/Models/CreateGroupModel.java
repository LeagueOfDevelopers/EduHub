package com.example.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 26.01.2018.
 */

public class CreateGroupModel {
    @SerializedName("title")
    @Expose
    private String title;
    @SerializedName("description")
    @Expose
    private String description;
    @SerializedName("tags")
    @Expose
    private List<String> tags = null;
    @SerializedName("size")
    @Expose
    private Integer size;
    @SerializedName("moneyPerUser")
    @Expose
    private Double moneyPerUser;
    @SerializedName("groupType")
    @Expose
    private Integer groupType;
    @SerializedName("isPrivate")
    @Expose
    private Boolean isPrivate;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public List<String> getTags() {
        return tags;
    }

    public void setTags(List<String> tags) {
        this.tags = tags;
    }

    public Integer getSize() {
        return size;
    }

    public void setSize(Integer size) {
        this.size = size;
    }

    public Double getMoneyPerUser() {
        return moneyPerUser;
    }

    public void setMoneyPerUser(Double moneyPerUser) {
        this.moneyPerUser = moneyPerUser;
    }

    public Integer getGroupType() {
        return groupType;
    }

    public void setGroupType(Integer groupType) {
        this.groupType = groupType;
    }

    public Boolean getIsPrivate() {
        return isPrivate;
    }

    public void setIsPrivate(Boolean isPrivate) {
        this.isPrivate = isPrivate;
    }

}

