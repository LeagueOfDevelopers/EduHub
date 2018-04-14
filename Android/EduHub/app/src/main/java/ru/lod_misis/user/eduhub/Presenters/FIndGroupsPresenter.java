package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IFindGroupsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IFindGroupsView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

/**
 * Created by User on 05.04.2018.
 */

public class FIndGroupsPresenter implements IFindGroupsPresenter {
    private IFindGroupsView findGroupsView;
    EduHubApi eduHubApi;
    ArrayList<Group> groups;
    public FIndGroupsPresenter(IFindGroupsView findGroupsView) {
        this.findGroupsView = findGroupsView;
    }

    @Override
    public void findGroupsWithoutFilters(String title,Context context) {
        eduHubApi= RetrofitBuilder.getApi(context);
        eduHubApi.findGroupsWithOutFilters(title)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{groups=(ArrayList<Group>) next;},
                        throwable -> {
                            Log.e("FindGroupsErr",throwable.toString());},
                        ()->{findGroupsView.getGroups(groups);});
    }

    @Override
    public void findGroupsWithFilters(Double minPrice, Double maxPrice, String title,
                                      ArrayList<String> tags, String type, Boolean formed,Context context) {
        eduHubApi=RetrofitBuilder.getApi(context);
        switch (type){
            case "":{type="Default";
                break;}
            case "Лекция":{type="Lecture";
                break;}
            case "Мастер класс":{type="MasterClass";
                break;}
            case "Семинар":{type="Seminar";
                break;}
        }
        if(maxPrice==0) {

        }
        if(type.equals("")){
            }

        if(tags.size()==0){

        }
        findGroupsView.showLoading();
        eduHubApi.findGroupsWithFilters(minPrice,maxPrice,title,tags,type,formed)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{groups=(ArrayList<Group>) next;},
                        throwable -> {Log.e("FindGroupsErr",throwable.toString());
                            findGroupsView.getError(throwable);},
                        ()->{findGroupsView.getGroups(groups);
                findGroupsView.stopLoading();});

    }
}
