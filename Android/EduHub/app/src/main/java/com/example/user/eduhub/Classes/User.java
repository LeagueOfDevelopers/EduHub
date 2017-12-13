package com.example.user.eduhub.Classes;

import java.util.ArrayList;
import java.util.Date;

/**
 * Created by user on 06.12.2017.
 */

public class User {
    private String email;
    private String password;
    private String login;
    private int qualification;
    private ArrayList<String> skils;
    private String description;
    private Date birthYear;
    private String sex;

    public String getEmail(){
        return email;
    }
    public void setEmail(String email){
        this.email=email;
    }
    public String getPassword(){
        return password;
    }
    public void setPassword(String password){
        this.password=password;
    }
    public String getLogin(){
        return login;
    }
    public void setLogin(String login){
        this.login=login;
    }
    public int getQualification(){
        return qualification;
    }
    public void setQualification(int qualification){
        this.qualification=qualification;
    }
    public ArrayList<String> getSkils(){
        return skils;
    }
    public void setSkils(ArrayList<String> skils){
        this.skils=skils;
    }
    public String getDescription(){
        return description;
    }
    public void setDescription(String description){
        this.description=description;
    }
    public Date getBirthYear(){
        return birthYear;
    }
    public void setBirthYear(Date birthYear){
        this.birthYear=birthYear;
    }
    public String getSex(){
        return sex;
    }
    public void setSex(String sex){
        this.sex=sex;
    }

}