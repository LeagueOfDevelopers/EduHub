package com.example.user.eduhub.Fakes;

import android.util.Log;

import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Interfaces.Presenters.IGroupRepository;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;

import java.util.ArrayList;

/**
 * Created by User on 16.01.2018.
 */

public class FakeGroupRepository  implements IGroupRepository{
    private IGroupListView groupListView;

    public FakeGroupRepository(IGroupListView groupListView) {

        this.groupListView=groupListView;
    }
    @Override
    public void loadGroups() {
        ArrayList<Group> groups=new ArrayList<>();
        GroupInfo groupInfo=new GroupInfo();
        ArrayList<String> tags=new ArrayList<>();
        tags.add("C#");
        tags.add("Java");
        groupInfo.setDescription("Test");
        groupInfo.setSize(5);
        groupInfo.setTags(tags);
        groupInfo.setMoneyPerUser(500);
        groupInfo.setGroupType(3);
        groupInfo.setTitle("It's Fake!!!");
        groupInfo.setId("93d08fd5-c101-42d4-8811-8e48f2434304");
        Group group=new Group();
        group.setGroupInfo(groupInfo);
        group.setNumberOfMembers(1);
        for(int i=0;i<10;i++){
            groups.add(group);
        }
        Log.d("CREATE GROUPS",groups.size()+"");
        groupListView.getGroups(groups);
    }
}