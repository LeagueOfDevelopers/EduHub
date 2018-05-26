package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.View.IGroupListView;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IGroupRepository;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void loadAllGroupsForUsers(Context context) {
        eduHubApi= RetrofitBuilder.getApi(context);
        disposable=eduHubApi.getGroups()
                .subscribeOn(Schedulers.io())//вверх
                .observeOn(AndroidSchedulers.mainThread())//вниз

                .subscribe(
                        next->{
                            groups=(ArrayList<Group>) next.getFillingGroups();},
                        error->{
                            Log.d("ERROR",error+"");
                        },
                        ()-> {
                            groupListView.getGroups(groups);

                        });

    }

    @Override
    public void loadAllGroupsForTeachers(Context context) {
        eduHubApi= RetrofitBuilder.getApi(context);
        disposable=eduHubApi.getGroups()
                .subscribeOn(Schedulers.io())//вверх
                .observeOn(AndroidSchedulers.mainThread())//вниз

                .subscribe(
                        next->{
                            groups=(ArrayList<Group>) next.getFullGroups();},
                        error->{
                            Log.d("ERROR",error+"");
                        },
                        ()-> {
                            groupListView.getGroups(groups);

                        });
    }

    @Override
    public void loadUsersGroup(String token,String userId, Context context) {
        eduHubApi= RetrofitBuilder.getApi(context);
        disposable=eduHubApi.getUsersGroup("Bearer "+token,userId)
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


                        });
    }
}
