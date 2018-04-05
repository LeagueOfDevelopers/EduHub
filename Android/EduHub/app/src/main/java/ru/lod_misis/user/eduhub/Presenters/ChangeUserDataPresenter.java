package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IChangeUsersDataPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IChangeUsersDataView;
import ru.lod_misis.user.eduhub.Models.UserProfile.RefactorUserRequestModel;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;


import io.reactivex.Observable;
import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 16.02.2018.
 */

public class ChangeUserDataPresenter implements IChangeUsersDataPresenter {
    IChangeUsersDataView changeUsersDataView;
    Observable<String> changeUsersRole;


    public ChangeUserDataPresenter(IChangeUsersDataView changeUsersDataView) {
        this.changeUsersDataView = changeUsersDataView;
    }

    @Override
    public void changeUsersData(String token,String name, String aboutUser, ArrayList<String> contacts, Integer birthYear, String avatarLink, String sex,Boolean isTeacher,String[] skills) {
        Observable changeAboutUser=Observable.empty();
        Observable changeUsersAvatarLink=Observable.empty();
        RefactorUserRequestModel refactorUserRequestModel=new RefactorUserRequestModel();
        refactorUserRequestModel.setUserName(name);
        refactorUserRequestModel.setAboutUser(aboutUser);
        refactorUserRequestModel.setAvatarLink(avatarLink);
        refactorUserRequestModel.setBirthYear(birthYear);
        refactorUserRequestModel.setContacts(contacts);
        if(sex.equals("Мужской")){
            refactorUserRequestModel.setGender("Man");
        }else{
            if (sex.equals("Женский")) {
                refactorUserRequestModel.setGender("Woman");
            }
            if(sex.equals("")){
                refactorUserRequestModel.setGender("Unknown");
            }
        }

        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        Observable changeUserName =eduHubApi.changesUserName("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .subscribeOn(Schedulers.io());
        if(aboutUser.equals("")){}else{
         changeAboutUser=eduHubApi.changesAboutUser("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .subscribeOn(Schedulers.io());}
        Observable changeUserConatcts=eduHubApi.changesUsersContacts("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .subscribeOn(Schedulers.io());
        Observable changeUserBirthYear=eduHubApi.changesUsersBirthYear("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .subscribeOn(Schedulers.io());
        Observable changeUsersGender=eduHubApi.changesUsersGender("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .subscribeOn(Schedulers.io());
        if(!avatarLink.equals("")){
         changeUsersAvatarLink=eduHubApi.changesUserAvatar("Bearer "+token,refactorUserRequestModel)
                .toObservable()
                .subscribeOn(Schedulers.io());}else{

        }

        if(isTeacher){
         changeUsersRole=eduHubApi.becomeTeacher("Bearer "+token)
        .subscribeOn(Schedulers.io())
        .toObservable();}else{
             changeUsersRole=eduHubApi.becomeSimpleUser("Bearer "+token)
                    .subscribeOn(Schedulers.io())
                    .toObservable();
        }
        Observable.merge(changeAboutUser,changeUserBirthYear,changeUserConatcts,changeUserName)

                .mergeWith(Observable.merge(changeUsersRole,changeUsersGender,changeUsersAvatarLink))
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{},
                        throwable -> {
                            Log.e("ChangeUserData",throwable.toString());},
                        ()->{changeUsersDataView.getResponse();});




    }
}
