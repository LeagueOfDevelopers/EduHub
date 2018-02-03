package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.UserProfile.UserProfile;

/**
 * Created by User on 31.01.2018.
 */

public interface ISearchResponse  {

    void getResult(UserProfile userProfile);
    void getError(Throwable error);
}
