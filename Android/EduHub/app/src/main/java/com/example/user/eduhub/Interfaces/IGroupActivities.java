package com.example.user.eduhub.Interfaces;

import com.example.user.eduhub.Models.Group.GroupInfo;

import java.util.ArrayList;

/**
 * Created by user on 21.12.2017.
 */

public interface IGroupActivities {
     boolean CreateGroup (GroupInfo group);
     ArrayList<GroupInfo> loadGroups();
}
