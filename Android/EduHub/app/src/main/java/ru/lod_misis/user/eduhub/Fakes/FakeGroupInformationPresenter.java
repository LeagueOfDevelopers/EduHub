package ru.lod_misis.user.eduhub.Fakes;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IGroupInfirmationPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.Group.GroupInfo;
import ru.lod_misis.user.eduhub.Models.Group.Member;

import java.util.ArrayList;

/**
 * Created by User on 18.01.2018.
 */

public class FakeGroupInformationPresenter  implements IGroupInfirmationPresenter {
    public FakeGroupInformationPresenter(IGroupView groupInformationView) {
        this.groupInformationView = groupInformationView;
    }

    private IGroupView groupInformationView;
    @Override
    public void loadGroupInformation(String groupId) {
        Group group=new Group();
        ArrayList<Member> members=new ArrayList<>();
        Member member=new Member();
        member.setPaid(true);
        member.setRole(2);
        member.setUserId("123");

        Member member_=new Member();

        member.setName("Александр");
        for(int i=0;i<10;i++){
            members.add(member);
        }
        GroupInfo groupInfo=new GroupInfo();
        ArrayList<String> tags=new ArrayList<>();
        tags.add("C#");
        tags.add("Java");
        groupInfo.setDescription("Test");
        groupInfo.setSize(5);
        groupInfo.setTags(tags);
        groupInfo.setCost(500.0);
        groupInfo.setGroupType(3);
        groupInfo.setMemberAmount(5);
        groupInfo.setCourseStatus(0);
        groupInfo.setTitle("It's Fake!!!");
        groupInfo.setId("93d08fd5-c101-42d4-8811-8e48f2434304");
        group.setGroupInfo(groupInfo);
        group.setMembers(members);



        groupInformationView.getInformationAboutGroup(group);
    }
}
