package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.UserProfile.UserProfile;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Models.UserProfile.UserSearchProfile;

import java.util.List;

/**
 * Created by User on 31.01.2018.
 */

public interface ISearchResponse extends IBaseView {

    void getResult(List<UserSearchProfile> userProfile);

}
