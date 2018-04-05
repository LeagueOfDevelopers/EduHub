package ru.lod_misis.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by User on 24.02.2018.
 */

public class RefactorGroupRequestModel {
    @SerializedName("groupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("groupDescription")
    @Expose
    private String groupDescription;
    @SerializedName("groupTags")
    @Expose
    private ArrayList<String> groupTags;
    @SerializedName("groupSize")
    @Expose
    private Integer groupSize;
    @SerializedName("groupPrice")
    @Expose
    private Double groupPrice;
    @SerializedName("groupType")
    @Expose
    private String groupType;
    @SerializedName("isPrivate")
    @Expose
    private Boolean isPrivate;

    public String getGroupTitle() {
        return groupTitle;
    }

    public void setGroupTitle(String groupTitle) {
        this.groupTitle = groupTitle;
    }

    public String getGroupDescription() {
        return groupDescription;
    }

    public void setGroupDescription(String groupDescription) {
        this.groupDescription = groupDescription;
    }

    public ArrayList<String> getGroupTags() {
        return groupTags;
    }

    public void setGroupTags(ArrayList<String> groupTags) {
        this.groupTags = groupTags;
    }

    public Integer getGroupSize() {
        return groupSize;
    }

    public void setGroupSize(Integer groupSize) {
        this.groupSize = groupSize;
    }

    public Double getGroupPrice() {
        return groupPrice;
    }

    public void setGroupPrice(Double groupPrice) {
        this.groupPrice = groupPrice;
    }

    public String getGroupType() {
        return groupType;
    }

    public void setGroupType(String groupType) {
        this.groupType = groupType;
    }

    public Boolean getPrivate() {
        return isPrivate;
    }

    public void setPrivate(Boolean aPrivate) {
        isPrivate = aPrivate;
    }
}
