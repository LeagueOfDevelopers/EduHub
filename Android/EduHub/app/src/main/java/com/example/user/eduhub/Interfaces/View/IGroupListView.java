package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.Group.Group;

import java.util.ArrayList;

/**
 * Created by User on 16.01.2018.
 */

public interface IGroupListView extends IBaseView {
    void getGroups(ArrayList<Group> groups);

}
