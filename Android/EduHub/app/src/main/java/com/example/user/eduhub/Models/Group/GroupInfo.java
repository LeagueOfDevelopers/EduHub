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
    @SerializedName("description")
    @Expose
    private String description;
    @SerializedName("tags")
    @Expose
    private List<String> tags = null;
    @SerializedName("groupType")
    @Expose
    private Integer groupType;
    @SerializedName("isPrivate")
    @Expose
    private Boolean isPrivate;
    @SerializedName("isActive")
    @Expose
    private Boolean isActive;
    @SerializedName("size")
    @Expose
    private Integer size;
    @SerializedName("moneyPerUser")
    @Expose
    private Integer moneyPerUser;

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

    public Boolean getIsActive() {
        return isActive;
    }

    public void setIsActive(Boolean isActive) {
        this.isActive = isActive;
    }

    public Integer getSize() {
        return size;
    }

    public void setSize(Integer size) {
        this.size = size;
    }

    public Integer getMoneyPerUser() {
        return moneyPerUser;
    }

    public void setMoneyPerUser(Integer moneyPerUser) {
        this.moneyPerUser = moneyPerUser;
    }


}

