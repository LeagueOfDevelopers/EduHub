package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.IGroupInfirmationPresenter;
import com.example.user.eduhub.Interfaces.View.IGroupView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 17.01.2018.
 */

public class GroupInformationPresenter implements IGroupInfirmationPresenter {
    private IGroupView groupView;
    Disposable disposable;
    Group group;

    public GroupInformationPresenter(IGroupView groupView) {
        this.groupView = groupView;
    }

    @Override
    public void loadGroupInformation(String groupId) {
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        disposable= eduHubApi.getInformationAbotGroup(groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                        response->{
                            group=response;},
                        error->{
                            Log.e("ERROR",error+"");},
                        ()->{groupView.getInformationAboutGroup(group);});
    }
}
