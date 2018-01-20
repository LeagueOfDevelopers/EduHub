package com.example.user.eduhub.Models.Group;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by User on 18.01.2018.
 */

public class Member_ {
    @SerializedName("member")
    @Expose
    private Member member;
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("avatarLink")
    @Expose
    private String avatarLink;

    public Member getMember() {
        return member;
    }

    public void setMember(Member member) {
        this.member = member;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getAvatarLink() {
        return avatarLink;
    }

    public void setAvatarLink(String avatarLink) {
        this.avatarLink = avatarLink;
    }
}
