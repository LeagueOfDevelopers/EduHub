package com.example.user.eduhub.Interfaces;

import com.example.user.eduhub.Classes.User;

/**
 * Created by user on 16.12.2017.
 */

public interface IAccountActivities {
    void UserLogin(String email,String password);
    void UserRegistration(String email,String password,String name,Boolean isTeacher);
}
