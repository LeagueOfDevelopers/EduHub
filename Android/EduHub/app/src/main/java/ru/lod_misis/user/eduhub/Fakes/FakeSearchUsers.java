package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ISearchUserPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ISearchResponse;
import ru.lod_misis.user.eduhub.Models.UserProfile.Review;
import ru.lod_misis.user.eduhub.Models.UserProfile.TeacherProfile;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserSearchProfile;

import java.util.ArrayList;

/**
 * Created by User on 07.02.2018.
 */

public class FakeSearchUsers implements ISearchUserPresenter {
        ISearchResponse searchResponse;

    public FakeSearchUsers(ISearchResponse searchResponse) {
        this.searchResponse = searchResponse;
    }

    @Override
    public void searchUser(String name, Context context) {
        UserSearchProfile userSearchProfile=new UserSearchProfile();
        userSearchProfile.setEmail("fake@fake.fake");
        userSearchProfile.setId("93d08fd5-c101-42d4-8811-8e48f2434304");
        userSearchProfile.setInvited(false);
        userSearchProfile.setIsTeacher(true);
        userSearchProfile.setUsername("Шурик");
        TeacherProfile teacherProfile=new TeacherProfile();
        teacherProfile.setSkills(new ArrayList<String>());
        Review review=new Review();
        review.setFromUser("Ярослав");
        review.setDate("25.05.11");
        review.setText("Так себе препод,но человек хороший");
        ArrayList<Review> reviews=new ArrayList<>();
        for (int i=0;i<10;i++){
            reviews.add(review);
        }
        teacherProfile.setReviews(reviews);

        ArrayList<UserSearchProfile> userSearchProfiles=new ArrayList<>();
        for (int i=0;i<10;i++){
            userSearchProfiles.add(userSearchProfile);
        }
        Log.d("СюдаПроходит?fakesearch",userSearchProfiles.size()+"");
        searchResponse.getResult(userSearchProfiles);
    }

    @Override
    public void searchUserForInvitation(String name, String groupId, Context context,Boolean isTeacher) {
        searchUser(name,context);
    }
}
