package ru.lod_misis.user.eduhub.Models.UserProfile;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Created by User on 16.02.2018.
 */

public class RefactorUserRequestModel {

    @SerializedName("name")
    @Expose
    private String name;
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
    private String birthYear;

    @SerializedName("gender")
    @Expose
    private String gender;

    public String getUserName() {
        return name;
    }

    public void setUserName(String userName) {
        this.name = userName;
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

    public String getBirthYear() {
        return birthYear;
    }

    public void setBirthYear(String birthYear) {
        this.birthYear = birthYear;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }
}
