package com.example.user.eduhub.Interfaces;

import com.example.user.eduhub.Classes.Group;
import com.example.user.eduhub.Fragments.CreateGroupFragment;

import java.util.ArrayList;

/**
 * Created by user on 21.12.2017.
 */

public interface IGroupActivities {
     boolean CreateGroup (Group group);
     ArrayList<Group> loadGroups();
}
