package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 06.02.2018.
 */

public class UserSearchProfile {
    @SerializedName("invited")
    @Expose
    private Boolean invited;
    @SerializedName("username")
    @Expose
    private String username;
    @SerializedName("isTeacher")
    @Expose
    private Boolean isTeacher;
    @SerializedName("id")
    @Expose
    private String id;
    @SerializedName("email")
    @Expose
    private String email;
    @SerializedName("avatarLink")
    @Expose
    private Object avatarLink;

    public Boolean getInvited() {
        return invited;
    }

    public void setInvited(Boolean invited) {
        this.invited = invited;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public Boolean getIsTeacher() {
        return isTeacher;
    }

    public void setIsTeacher(Boolean isTeacher) {
        this.isTeacher = isTeacher;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public Object getAvatarLink() {
        return avatarLink;
    }

    public void setAvatarLink(Object avatarLink) {
        this.avatarLink = avatarLink;
    }

}
