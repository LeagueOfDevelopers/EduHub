package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Interfaces.Presenters.IChangeGroupSettingsPresenter;
import com.example.user.eduhub.Interfaces.View.IChangeGroupSettingsView;
import com.example.user.eduhub.Models.Group.RefactorGroupRequestModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;


import io.reactivex.Observable;
import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 24.02.2018.
 */
public class ChangeGroupSettingsPresenter implements IChangeGroupSettingsPresenter {
    IChangeGroupSettingsView changeGroupSettingsView;

    public ChangeGroupSettingsPresenter(IChangeGroupSettingsView changeGroupSettingsView) {
        this.changeGroupSettingsView = changeGroupSettingsView;
    }

    @Override
    public void chngeGroupSettings(String token,String groupId, String groupName, Integer size, Double cost, String typeOfEducation, ArrayList<String> tags,String description,Boolean isPrivate) {
        Log.d("rrr",token);
        RefactorGroupRequestModel refactorGroupRequestModel=new RefactorGroupRequestModel();
        refactorGroupRequestModel.setGroupTitle(groupName);
        refactorGroupRequestModel.setGroupDescription(description);
        refactorGroupRequestModel.setGroupPrice(cost);
        refactorGroupRequestModel.setGroupSize(size);
        refactorGroupRequestModel.setGroupTags(tags);
        if(typeOfEducation.equals("Лекция")){
            refactorGroupRequestModel.setGroupType("Lecture");
        }else{
            if(typeOfEducation.equals("Семинар")){
                refactorGroupRequestModel.setGroupType("Seminar");
            }
            if(typeOfEducation.equals("Мастер класс")){
                refactorGroupRequestModel.setGroupType("MasterClass");
            }
        }
        refactorGroupRequestModel.setPrivate(isPrivate);
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        Observable changeTitle=eduHubApi.changeGroupTitle("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable changeDescription=eduHubApi.changeGroupDescription("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable changePrice=eduHubApi.changeGroupPrice("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable changeprivacy=eduHubApi.changeGroupPrivacy("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable changeSize=eduHubApi.changeGroupSize("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable changeTags=eduHubApi.changeGroupTags("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable changeType=eduHubApi.changeGroupType("Bearer "+token,groupId,refactorGroupRequestModel)
                .subscribeOn(Schedulers.io())
                .toObservable();
        Observable.merge(changeTitle,changeDescription,changePrice,changeprivacy)
                .mergeWith(Observable.merge(changeSize,changeTags,changeType))
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{},
                        throwable -> {Log.d("ChangeSettingsGroup",throwable.toString());
                        },
                        ()->{changeGroupSettingsView.getResponse();});
    }
}
