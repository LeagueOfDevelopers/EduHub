package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.Presenters.IGroupInfirmationPresenter;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.Group.Member_;

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
        ArrayList<Member_> members=new ArrayList<>();
        Member member=new Member();
        member.setPaid(true);
        member.setMemberRole(2);
        Member_ member_=new Member_();
        member_.setMember(member);
        member_.setName("Александр");
        for(int i=0;i<10;i++){
            members.add(member_);
        }
        GroupInfo groupInfo=new GroupInfo();
        ArrayList<String> tags=new ArrayList<>();
        tags.add("C#");
        tags.add("Java");
        groupInfo.setDescription("Test");
        groupInfo.setSize(5);
        groupInfo.setTags(tags);
        groupInfo.setMoneyPerUser(500);
        groupInfo.setId(groupId);
        group.setNumberOfMembers(1);
        group.setGroupInfo(groupInfo);
        group.setMembers(members);



        groupInformationView.getInformationAboutGroup(group);
    }
}
