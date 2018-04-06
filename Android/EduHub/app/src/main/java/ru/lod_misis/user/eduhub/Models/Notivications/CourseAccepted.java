package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class CourseAccepted {
    @SerializedName("GroupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("GroupId")
    @Expose
    private String groupId;

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
}
