package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.IAccountActivities;

/**
 * Created by User on 23.12.2017.
 */

public class FakeAcocuntActivities implements IAccountActivities {
    @Override
    public User UserLogin(String email, String password ) {
        User user=new User();
        user.setEmail(email);
        user.setPassword(password);
        user.setRole("Admin");
        user.setName(email);
        return user;
    }

    @Override
    public Boolean UserRegistration(String email, String password, String name, Boolean isTeacher) {
        return null;
    }


}
