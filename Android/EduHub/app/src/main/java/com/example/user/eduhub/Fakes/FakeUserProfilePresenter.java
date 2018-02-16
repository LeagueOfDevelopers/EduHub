package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.Presenters.IUserProfilePresenter;
import com.example.user.eduhub.Interfaces.View.IUserProfileView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.Group.GroupInfo;
import com.example.user.eduhub.Models.Group.Member;
import com.example.user.eduhub.Models.UserProfile.Review;
import com.example.user.eduhub.Models.UserProfile.TeacherProfile;
import com.example.user.eduhub.Models.UserProfile.UserProfile;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;

import java.util.ArrayList;

/**
 * Created by User on 30.01.2018.
 */

public class FakeUserProfilePresenter implements IUserProfilePresenter {
    private IUserProfileView userProfileView;

    public FakeUserProfilePresenter(IUserProfileView userProfileView) {
        this.userProfileView = userProfileView;
    }

    @Override
    public void loadUserProfile(String token,String userId) {
        UserProfileResponse userProfileResponse=new UserProfileResponse();
        UserProfile userProfile=new UserProfile();
        userProfile.setEmail("lod.misis.ru");
        userProfile.setIsTeacher(true);
        userProfile.setName("Александр");
        TeacherProfile teacherProfile=new TeacherProfile();
        ArrayList<String> contacts=new ArrayList<>();
        contacts.add("id12342123");
        contacts.add("@user");
        userProfile.setContacts(contacts);
        ArrayList<String> skils=new ArrayList<>();
        skils.add("JS");
        skils.add("Android");
        teacherProfile.setSkills(skils);
        Review review=new Review();
        review.setFromUser("Ярослав");
        review.setText("Так себе препод,но человек хороший");
        ArrayList<Review> reviews=new ArrayList<>();
        for (int i=0;i<10;i++){
            reviews.add(review);
        }
        ArrayList<Group> groups=new ArrayList<>();
        Group group=new Group();
        ArrayList<Member> members=new ArrayList<>();
        Member member=new Member();
        member.setPaid(true);
        member.setMemberRole(2);
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
        groupInfo.setCost(500);
        groupInfo.setGroupType(3);
        groupInfo.setTitle("It's Fake!!!");
        groupInfo.setId(userId);
        groupInfo.setMemberAmount(1);
        groupInfo.setMemberAmount(2);
        group.setGroupInfo(groupInfo);
        group.setMembers(members);
        for (int i=0;i<10;i++){
            groups.add(group);
        }
        userProfile.setIsMan(true);
        userProfile.setBirthYear(1999);
        userProfile.setAboutUser("Начинающий Андроид разработчик");
        teacherProfile.setReviews(reviews);
        teacherProfile.setJobExp(groups);

        userProfileResponse.setTeacherProfile(teacherProfile);
        userProfileResponse.setUserProfile(userProfile);



        userProfileView.getUserProfile(userProfileResponse);
    }
}
