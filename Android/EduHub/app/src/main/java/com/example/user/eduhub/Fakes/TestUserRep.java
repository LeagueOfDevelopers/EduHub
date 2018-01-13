package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.IUserRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import java.util.ArrayList;

/**
 * Created by user on 07.12.2017.
 */

public class TestUserRep implements IUserRepository {
    @Override
    public ArrayList<User> LoadUsers() {

        return null;
    }

    @Override
    public ArrayList<User> AddUser(User user) {
        ArrayList<User> users=LoadUsers();
        users.add(user);
        return users;
    }
}
