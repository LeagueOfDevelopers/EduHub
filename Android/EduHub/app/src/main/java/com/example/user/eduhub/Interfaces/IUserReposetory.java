package com.example.user.eduhub.Interfaces;

import com.example.user.eduhub.Classes.User;

import java.util.ArrayList;

/**
 * Created by user on 06.12.2017.
 */

public interface IUserReposetory {
    ArrayList<User> LoadUsers();
    void SaveUsers(ArrayList<User> users);
}
