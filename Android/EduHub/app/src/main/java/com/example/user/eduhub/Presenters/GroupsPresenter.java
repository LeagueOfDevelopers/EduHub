package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Interfaces.Presenters.IGroupRepository;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;


/**
 * Created by User on 16.01.2018.
 */

public class GroupsPresenter implements IGroupRepository {
    EduHubApi eduHubApi;
    Disposable disposable;
    ArrayList<Group> groups;



    private IGroupListView groupListView;

    public GroupsPresenter(IGroupListView groupListView) {

        this.groupListView=groupListView;
    }

    @Override
    public void loadAllGroups() {
        eduHubApi= RetrofitBuilder.getApi();
        disposable=eduHubApi.getGroups()
                .subscribeOn(Schedulers.io())//вверх
                .observeOn(AndroidSchedulers.mainThread())//вниз

                .subscribe(
                        next->{
                            groups=next.getGroups();},
                        error->{
                            Log.d("ERROR",error+"");
                        },
                        ()-> {
                            groupListView.getGroups(groups);
                            disposable.dispose();
                        });

    }

    @Override
    public void loadUsersGroup(String token) {
        Log.d("TOKEN",token);
        eduHubApi= RetrofitBuilder.getApi();
        disposable=eduHubApi.getUsersGroup("Bearer "+token)
                .subscribeOn(Schedulers.io())//вверх
                .observeOn(AndroidSchedulers.mainThread())//вниз

                .subscribe(
                        next->{

                            groups=next.getGroups();},
                        error->{
                            Log.e("ERROR",error+"");

                        },
                        ()-> {
                            groupListView.getGroups(groups);
                            disposable.dispose();

                        });
    }
}
