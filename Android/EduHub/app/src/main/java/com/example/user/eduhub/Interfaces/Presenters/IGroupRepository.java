package com.example.user.eduhub.Interfaces.Presenters;

import com.example.user.eduhub.Models.Group.GroupInfo;

import java.util.ArrayList;

/**
 * Created by user on 21.12.2017.
 */

public interface IGroupRepository {

     void loadAllGroups();
     void loadUsersGroup(String token);
}
