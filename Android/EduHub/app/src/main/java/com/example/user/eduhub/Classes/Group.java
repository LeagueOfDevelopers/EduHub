package com.example.user.eduhub.Classes;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;

/**
 * Created by user on 13.12.2017.
 */

public class Group {
    private String name;
    private int maxUsers;
    private int usersNow;
    private boolean privacy;
    private int cost;
    private TypeOfEducation typeOfEducation;
    private ArrayList<String> tags;

    public Group(String name,int maxUsers,ArrayList<String> tags,int cost,TypeOfEducation typeOfEducation,boolean privacy){
        this.name=name;
        this.maxUsers=maxUsers;
        this.typeOfEducation=typeOfEducation;
        this.tags=tags;
       this.cost=cost;
       this.privacy=privacy;

    }
    public String getName(){
        return name;
    }
    public void setName(String name){
        this.name=name;
    }
    public int getMaxUsers(){
        return maxUsers;
    }
    public void setMaxUsers(int maxUsers){
        this.maxUsers=maxUsers;
    }
    public int getUsersNow(){
        return usersNow;
    }
    public void setUsersNow(int usersNow){
        this.usersNow=usersNow;
    }
    public ArrayList<String> getTags(){
        return tags;
    }
    public void setTags(ArrayList<String> tags){
        this.tags=tags;
    }

    public int getCost(){
        return cost;
    }
    public void setCost(int cost){
        this.cost=cost;
    }
    public TypeOfEducation getTypeOfEducation(){
        return typeOfEducation;
    }
    public void setTypeOfEducation(TypeOfEducation typeOfEducation){
        this.typeOfEducation=typeOfEducation;
    }
    public boolean getPrivacy(){
        return privacy;
    }
    public void setPrivacy(boolean privacy){
        this.privacy=privacy;
    }


}
