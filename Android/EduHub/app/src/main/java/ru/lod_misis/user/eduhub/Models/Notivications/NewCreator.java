package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class NewCreator {
    @SerializedName("GroupId")
    @Expose
    private String groupId;
    @SerializedName("GroupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("ExCreator")
    @Expose
    private String exCreator;
    @SerializedName("ExCreatorUsername")
    @Expose
    private String exCreatorUsername;
    @SerializedName("NewCreator")
    @Expose
    private String newCreator;
    @SerializedName("NewCreatorUsername")
    @Expose
    private String newCreatorUsername;

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }

    public String getGroupTitle() {
        return groupTitle;
    }

    public void setGroupTitle(String groupTitle) {
        this.groupTitle = groupTitle;
    }

    public String getExCreator() {
        return exCreator;
    }

    public void setExCreator(String exCreator) {
        this.exCreator = exCreator;
    }

    public String getExCreatorUsername() {
        return exCreatorUsername;
    }

    public void setExCreatorUsername(String exCreatorUsername) {
        this.exCreatorUsername = exCreatorUsername;
    }

    public String getNewCreator() {
        return newCreator;
    }

    public void setNewCreator(String newCreator) {
        this.newCreator = newCreator;
    }

    public String getNewCreatorUsername() {
        return newCreatorUsername;
    }

    public void setNewCreatorUsername(String newCreatorUsername) {
        this.newCreatorUsername = newCreatorUsername;
    }
}
