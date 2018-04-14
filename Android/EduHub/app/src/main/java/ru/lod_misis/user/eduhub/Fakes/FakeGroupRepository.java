package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.View.IGroupListView;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IGroupRepository;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.GroupInfo;

import java.util.ArrayList;

/**
 * Created by User on 16.01.2018.
 */

public class FakeGroupRepository  implements IGroupRepository {
    private IGroupListView groupListView;

    public FakeGroupRepository(IGroupListView groupListView) {

        this.groupListView=groupListView;
    }
    @Override
    public void loadAllGroupsForUsers(Context context) {
        ArrayList<Group> groups=new ArrayList<>();
        GroupInfo groupInfo=new GroupInfo();
        ArrayList<String> tags=new ArrayList<>();
        tags.add("C#");
        tags.add("Java");
        tags.add("Javascript");
        tags.add("JS");

        groupInfo.setDescription("Test");
        groupInfo.setSize(5);
        groupInfo.setTags(tags);
        groupInfo.setCost(500.0);
        groupInfo.setGroupType(3);
        groupInfo.setMemberAmount(5);
        groupInfo.setTitle("It's Fake!!!");
        groupInfo.setId("93d08fd5-c101-42d4-8811-8e48f2434304");

        Group group=new Group();
        group.setGroupInfo(groupInfo);
        for(int i=0;i<10;i++){
            groups.add(group);
        }
        Log.d("CREATE GROUPS",groups.size()+"");
        groupListView.getGroups(groups);
    }

    @Override
    public void loadAllGroupsForTeachers( Context context) {
        loadAllGroupsForUsers(context);
    }

    @Override
    public void loadUsersGroup(String token,String userId, Context context) {
        loadAllGroupsForUsers(  context);
    }
}
