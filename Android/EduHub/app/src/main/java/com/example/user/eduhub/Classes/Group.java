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
    private Date deadLine;
    private ArrayList<String> tags;
    public Group(String name,int maxUsers,int usersNow,ArrayList<String> tags,Date deadLine){
        this.name=name;
        this.maxUsers=maxUsers;
        this.usersNow=usersNow;
        this.tags=tags;
        this.deadLine=deadLine;

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
    public Date getDeadLine(){
        return deadLine;
    }

}
