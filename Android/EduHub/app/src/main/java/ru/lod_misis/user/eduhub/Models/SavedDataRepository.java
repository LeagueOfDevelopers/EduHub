package ru.lod_misis.user.eduhub.Models;

import android.content.SharedPreferences;

import com.auth0.android.jwt.JWT;

/**
 * Created by User on 10.01.2018.
 */

public class SavedDataRepository {
    public User loadSavedData(SharedPreferences sPref){
        User user=new User();
        user.setRole(sPref.getString("ROLE",null));
        user.setTeacher(sPref.getBoolean("ROLE2",false));
        user.setUserId(sPref.getString("ID",null));
        user.setToken(sPref.getString("TOKEN",null));
        user.setAvatarLink(sPref.getString("AVATARLINK",null));
        user.setName(sPref.getString("NAME",null));
        user.setEmail(sPref.getString("EMAIL",null));
        return user;
    }
    public Integer loadExp(SharedPreferences sPref){
        Integer exp=sPref.getInt("EXP",0);
        return exp;
    }
    public Boolean loadCheckButtonResult(SharedPreferences sPref){
        Boolean bool=sPref.getBoolean("CheckButton",false);
        return bool;
    }
    public void SaveUser(String token,String name,String avatarLink,String email,Boolean role,SharedPreferences sPref){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        JWT jwt = new JWT(token);
        editor.putString("ROLE",jwt.getClaim("Role").asString());
        editor.putString("ID",jwt.getClaim("UserId").asString());
        editor.putInt("EXP",jwt.getClaim("exp").asInt());
        editor.putBoolean("ROLE2",role);
        editor.putString("TOKEN",token);
        editor.putString("NAME",name);
        editor.putString("EMAIL",email);
        editor.commit();
    }
    public void SaveCheckButtonResult(Boolean bool,SharedPreferences sPref){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        editor.putBoolean("CheckButton",bool);
        editor.commit();
    }
}
