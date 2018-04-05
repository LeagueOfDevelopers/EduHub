package ru.lod_misis.user.eduhub.Interfaces.View;

import ru.lod_misis.user.eduhub.Models.UserProfile.UserSearchProfile;

import java.util.List;

/**
 * Created by User on 31.01.2018.
 */

public interface ISearchResponse extends IBaseView {

    void getResult(List<UserSearchProfile> userProfile);

}
