package com.example.user.eduhub.Models.Group.Teacher;

import com.example.user.eduhub.Models.Credentials;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

/**
 * Created by User on 25.01.2018.
 */

public class Teacher {

    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("credentials")
    @Expose
    private Credentials credentials;
    @SerializedName("type")
    @Expose
    private String type;
    @SerializedName("isTeacher")
    @Expose
    private Boolean isTeacher;
    @SerializedName("teacherProfile")
    @Expose
    private TeacherProfile teacherProfile;
    @SerializedName("isActive")
    @Expose
    private Boolean isActive;
    @SerializedName("id")
    @Expose
    private String id;
    @SerializedName("avatarLink")
    @Expose
    private String avatarLink;
    @SerializedName("listOfInvitations")
    @Expose
    private List<ListOfInvitation> listOfInvitations = null;

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Credentials getCredentials() {
        return credentials;
    }

    public void setCredentials(Credentials credentials) {
        this.credentials = credentials;
    }

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public Boolean getIsTeacher() {
        return isTeacher;
    }

    public void setIsTeacher(Boolean isTeacher) {
        this.isTeacher = isTeacher;
    }

    public TeacherProfile getTeacherProfile() {
        return teacherProfile;
    }

    public void setTeacherProfile(TeacherProfile teacherProfile) {
        this.teacherProfile = teacherProfile;
    }

    public Boolean getIsActive() {
        return isActive;
    }

    public void setIsActive(Boolean isActive) {
        this.isActive = isActive;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getAvatarLink() {
        return avatarLink;
    }

    public void setAvatarLink(String avatarLink) {
        this.avatarLink = avatarLink;
    }

    public List<ListOfInvitation> getListOfInvitations() {
        return listOfInvitations;
    }

    public void setListOfInvitations(List<ListOfInvitation> listOfInvitations) {
        this.listOfInvitations = listOfInvitations;
    }

}