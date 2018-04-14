package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IChangeGroupSettingsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IChangeGroupSettingsView;
import ru.lod_misis.user.eduhub.Models.Group.RefactorGroupRequestModel;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void chngeGroupSettings(String token, String groupId, String groupName, Integer size, Double cost, String typeOfEducation, ArrayList<String> tags, String description, Boolean isPrivate, Context context) {
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
        EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
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
