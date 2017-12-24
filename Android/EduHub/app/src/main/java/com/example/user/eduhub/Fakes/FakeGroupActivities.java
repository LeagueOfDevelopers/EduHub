package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Classes.Group;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Interfaces.IGroupActivities;

import java.util.ArrayList;

/**
 * Created by user on 21.12.2017.
 */

public class FakeGroupActivities implements IGroupActivities {
    @Override
    public boolean CreateGroup(Group group) {
        return true;
    }

    @Override
    public ArrayList<Group> loadGroups() {
        ArrayList<Group> groups=new ArrayList<>();

        ArrayList<String> a=new ArrayList<>();
        a.add("#c#");
        a.add("#for beginers");
        groups.add(new Group("C# easy",8,a,25, TypeOfEducation.Lection,true));
        groups.add(new Group("Java для чайников",8,new ArrayList<String>(),10,TypeOfEducation.Vebinar,false));
        return groups;
    }
}
