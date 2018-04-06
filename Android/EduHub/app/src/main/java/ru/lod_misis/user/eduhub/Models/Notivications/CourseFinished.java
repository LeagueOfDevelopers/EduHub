package ru.lod_misis.user.eduhub.Models.Notivications;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.04.2018.
 */

public class CourseFinished {
    @SerializedName("GroupTitle")
    @Expose
    private String groupTitle;
    @SerializedName("GroupId")
    @Expose
    private String groupId;
    @SerializedName("TeacherName")
    @Expose
    private String teacherName;
    @SerializedName("TeacherId")
    @Expose
    private String teacherId;

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

    public String getTeacherName() {
        return teacherName;
    }

    public void setTeacherName(String teacherName) {
        this.teacherName = teacherName;
    }

    public String getTeacherId() {
        return teacherId;
    }

    public void setTeacherId(String teacherId) {
        this.teacherId = teacherId;
    }
}
