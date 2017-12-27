package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Fragments.LoginFragment;
import com.example.user.eduhub.Interfaces.IAccountActivities;

/**
 * Created by user on 16.12.2017.
 */

public class FakeReguest implements IAccountActivities{
    TestUserRep testUserRep=new TestUserRep();
    boolean flag=false;
    User checkUser;
    @Override
    public User UserLogin(String Login, String password) {
        for (User user:testUserRep.LoadUsers()
                ) {
            if(user.getEmail().equals(Login)){
                if(user.getPassword().equals(password)){
                    checkUser=user;
                    flag=true;
                }
            }

        }
        if(flag){

            return checkUser;


        }
        else{return null;}
    }



    @Override
    public Boolean UserRegistration(String login, String password, String name,Boolean isTeacher) {
        for (User user:testUserRep.LoadUsers()
                ) {
            if(user.getEmail().equals(login)){
                flag=true;
            }

        }
        if(flag){
            return false;
        }else{
            User newUser=new User();
            newUser.setEmail(login);
            newUser.setPassword(password);
            newUser.setName(name);


            LoginFragment loginFragment=new LoginFragment();

           return true;
        }
    }

}
