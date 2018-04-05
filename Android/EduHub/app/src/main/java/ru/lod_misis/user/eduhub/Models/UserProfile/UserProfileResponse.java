package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

/**
 * Created by User on 31.01.2018.
 */

public class UserProfileResponse implements Serializable {
    @SerializedName("userProfile")
    @Expose
    private UserProfile userProfile;
    @SerializedName("teacherProfile")
    @Expose
    private TeacherProfile teacherProfile;

    public UserProfile getUserProfile() {
        return userProfile;
    }

    public void setUserProfile(UserProfile userProfile) {
        this.userProfile = userProfile;
    }

    public TeacherProfile getTeacherProfile() {
        return teacherProfile;
    }

    public void setTeacherProfile(TeacherProfile teacherProfile) {
        this.teacherProfile = teacherProfile;
    }

}
