package com.example.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by User on 16.02.2018.
 */

public class RefactorUserRequestModel {

    @SerializedName("userName")
    @Expose
    private String userName;
    @SerializedName("aboutUser")
    @Expose
    private String aboutUser;
    @SerializedName("avatarLink")
    @Expose
    private String avatarLink;
    @SerializedName("contacts")
    @Expose
    private ArrayList<String> contacts;
    @SerializedName("birthYear")
    @Expose
    private Integer birthYear;

    @SerializedName("gender")
    @Expose
    private String gender;

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getAboutUser() {
        return aboutUser;
    }

    public void setAboutUser(String aboutUser) {
        this.aboutUser = aboutUser;
    }

    public String getAvatarLink() {
        return avatarLink;
    }

    public void setAvatarLink(String avatarLink) {
        this.avatarLink = avatarLink;
    }

    public ArrayList<String> getContacts() {
        return contacts;
    }

    public void setContacts(ArrayList<String> contacts) {
        this.contacts = contacts;
    }

    public Integer getBirthYear() {
        return birthYear;
    }

    public void setBirthYear(Integer birthYear) {
        this.birthYear = birthYear;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }
}
