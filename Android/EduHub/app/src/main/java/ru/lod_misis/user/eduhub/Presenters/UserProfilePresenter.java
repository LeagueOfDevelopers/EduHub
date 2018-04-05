package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IUserProfilePresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IUserProfileView;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 30.01.2018.
 */

public class UserProfilePresenter implements IUserProfilePresenter {
    private IUserProfileView userProfileView;

    public UserProfilePresenter(IUserProfileView userProfileView) {
        this.userProfileView = userProfileView;
    }

    @Override
    public void loadUserProfile(String token,String userId) {
        Log.d("UserProfileId",userId);
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.getUsersProfile("Bearer "+token,userId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(result->{
                    userProfileView.getUserProfile(result);},
                        error->{
                            Log.e("ERROR PROFILE",error+"");
                    userProfileView.getError(error);});
    }
}
