package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.Group;
import com.example.user.eduhub.Interfaces.IGroupActivities;

/**
 * Created by user on 21.12.2017.
 */

public class FakeGroupActivities implements IGroupActivities {
    @Override
    public boolean CreateGroup(Group group) {
        return true;
    }
}
