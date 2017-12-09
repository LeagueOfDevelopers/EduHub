package com.example.user.eduhub.Repository;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.IUserRepository;

import java.util.ArrayList;

/**
 * Created by user on 07.12.2017.
 */

public class TestUserRep implements IUserRepository {
    @Override
    public ArrayList<User> LoadUsers() {
        ArrayList<User> users=new ArrayList<>();
        User user=new User();
        user.setEmail("1");
        user.setPassword("1");
        users.add(user);
        return users ;
    }

    @Override
    public ArrayList<User> AddUser(User user) {
        ArrayList<User> users=LoadUsers();
        users.add(user);
        return users;
    }
}
