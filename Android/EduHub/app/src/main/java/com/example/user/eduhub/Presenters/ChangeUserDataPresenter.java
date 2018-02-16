package com.example.user.eduhub.Presenters;

import com.example.user.eduhub.Interfaces.Presenters.IChangeUsersDataPresenter;
import com.example.user.eduhub.Interfaces.View.IChangeUsersDataView;
import com.example.user.eduhub.Models.UserProfile.RefactorUserRequestModel;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;


import io.reactivex.Observable;
import io.reactivex.Single;
import io.reactivex.disposables.Disposable;
import io.reactivex.functions.Function5;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 16.02.2018.
 */

public class ChangeUserDataPresenter implements IChangeUsersDataPresenter {
    IChangeUsersDataView changeUsersDataView;

    public ChangeUserDataPresenter(IChangeUsersDataView changeUsersDataView) {
        this.changeUsersDataView = changeUsersDataView;
    }

    @Override
    public void changeUsersData(String token,String name, String aboutUser, ArrayList<String> contacts, Integer birthYear, String avatarLink, boolean sex) {
        RefactorUserRequestModel refactorUserRequestModel=new RefactorUserRequestModel();
        refactorUserRequestModel.setUserName(name);
        refactorUserRequestModel.setAboutUser(aboutUser);
        refactorUserRequestModel.setAvatarLink(avatarLink);
        refactorUserRequestModel.setBirthYear(birthYear);
        refactorUserRequestModel.setContacts(contacts);

        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        Observable<String> changeUserName =eduHubApi.changesUserName("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .observeOn(Schedulers.io());
        Observable<String> changeAboutUser=eduHubApi.changesAboutUser("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .observeOn(Schedulers.io());
        Observable<String> changeUserConatcts=eduHubApi.changesUsersContacts("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .observeOn(Schedulers.io());
        Observable<String> changeUserBirthYear=eduHubApi.changesUsersBirthYear("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .observeOn(Schedulers.io());
        Observable<String> changeUsersGender=eduHubApi.changesUsersGender("Bearer "+token,sex)
                .toObservable()
                .observeOn(Schedulers.io());
        Observable.combineLatest(changeAboutUser, changeUserBirthYear, changeUserName, changeUserConatcts, changeUsersGender, new Function5<String, String, String, String, String, Boolean>() {
            @Override
            public Boolean apply(String t1, String t2, String t3, String t4, String t5) throws Exception {
                return null;
            }
        }).subscribe();




    }
}
