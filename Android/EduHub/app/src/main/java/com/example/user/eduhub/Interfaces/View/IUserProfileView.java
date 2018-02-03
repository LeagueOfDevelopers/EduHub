package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.UserProfile;

/**
 * Created by User on 30.01.2018.
 */

public interface IUserProfileView extends IBaseView {
    void getUserProfile(UserProfile userProfile);
}
