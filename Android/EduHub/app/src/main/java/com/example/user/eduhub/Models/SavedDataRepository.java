package com.example.user.eduhub.Models;

import android.content.SharedPreferences;

/**
 * Created by User on 10.01.2018.
 */

public class SavedDataRepository {
    public User loadSavedData(SharedPreferences sPref){
        User user=new User();
        user.setRole(sPref.getString("ROLE",null));
        user.setUserId(sPref.getString("ID",null));
        user.setToken(sPref.getString("TOKEN",null));
        user.setAvatarLink(sPref.getString("AVATARLINK",null));
        user.setName(sPref.getString("NAME",null));
        user.setEmail(sPref.getString("EMAIL",null));
        return user;
    }
    public Boolean loadCheckButtonResult(SharedPreferences sPref){
        Boolean bool=sPref.getBoolean("CheckButton",false);
        return bool;
    }
}
