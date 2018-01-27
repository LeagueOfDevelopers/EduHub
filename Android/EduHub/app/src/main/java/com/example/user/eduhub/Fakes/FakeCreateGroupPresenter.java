package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Interfaces.Presenters.ICreateGroupPresenter;
import com.example.user.eduhub.Interfaces.View.ICreateGroupView;

import java.util.ArrayList;

/**
 * Created by User on 27.01.2018.
 */

public class FakeCreateGroupPresenter implements ICreateGroupPresenter {
    ICreateGroupView createGroupView;

    public FakeCreateGroupPresenter(ICreateGroupView createGroupView) {
        this.createGroupView = createGroupView;
    }

    @Override
    public void createGroup(String title, String description, ArrayList<String> tags, int size, int cost, TypeOfEducation groupType, Boolean isPrivate, String token) {
        createGroupView.getResponse();
    }
}
