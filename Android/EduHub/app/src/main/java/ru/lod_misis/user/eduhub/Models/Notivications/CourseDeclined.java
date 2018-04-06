package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class CourseDeclined {
    @SerializedName("GroupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("GroupId")
    @Expose
    private String groupId;
    @SerializedName("DeclinedName")
    @Expose
    private String declinedName;
    @SerializedName("DeclinedId")
    @Expose
    private String declinedId;

    public String getGroupTitle() {
        return groupTitle;
    }

    public void setGroupTitle(String groupTitle) {
        this.groupTitle = groupTitle;
    }

    public String getGroupId() {
        return groupId;
    }

    public void setGroupId(String groupId) {
        this.groupId = groupId;
    }

    public String getDeclinedName() {
        return declinedName;
    }

    public void setDeclinedName(String declinedName) {
        this.declinedName = declinedName;
    }

    public String getDeclinedId() {
        return declinedId;
    }

    public void setDeclinedId(String declinedId) {
        this.declinedId = declinedId;
    }
}
