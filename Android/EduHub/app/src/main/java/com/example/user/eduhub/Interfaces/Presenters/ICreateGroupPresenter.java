package com.example.user.eduhub.Interfaces.Presenters;

import com.example.user.eduhub.Classes.TypeOfEducation;

import java.util.ArrayList;

/**
 * Created by User on 26.01.2018.
 */

public interface ICreateGroupPresenter {
    void createGroup(String title, String description, ArrayList<String> tags, int size, Double cost, TypeOfEducation groupType,Boolean isPrivate,String token);
}
