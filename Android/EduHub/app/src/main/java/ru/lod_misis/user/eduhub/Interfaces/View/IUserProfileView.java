package ru.lod_misis.user.eduhub.Interfaces.View;

import ru.lod_misis.user.eduhub.Models.UserProfile.UserProfileResponse;

/**
 * Created by User on 30.01.2018.
 */

public interface IUserProfileView extends IBaseView {
    void getUserProfile(UserProfileResponse userProfile);
}
