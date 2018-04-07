package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ISignInUserToGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ISignInUserToGroupView;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;


/**
 * Created by User on 01.02.2018.
 */

public class SignInUserToGroupPresenter implements ISignInUserToGroupPresenter {
    ISignInUserToGroupView signInUserToGroupView;

    public SignInUserToGroupPresenter(ISignInUserToGroupView signInUserToGroupView) {
        this.signInUserToGroupView = signInUserToGroupView;
    }

    @Override
    public void signInUserToGroup(String token,String groupId) {
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.signInUserToGroup("Bearer "+token,groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{signInUserToGroupView.getResponse();},
                        error->{
                            Log.e("ERRORSIGNINUSERTOGROUP",error+"");});

    }

    @Override
    public void signInTeacherToGroup(String token, String groupId) {
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.signInTeacherToGroup("Bearer "+token,groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{signInUserToGroupView.getResponse();},
                        error->{
                            Log.e("ERRORSIGNINUSERTOGROUP",error+"");});
    }
}