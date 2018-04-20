package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IGroupInfirmationPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IGroupView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void loadGroupInformation(String groupId, Context context) {
        Log.d("GroupIfInPresenter",groupId);
        EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
        disposable= eduHubApi.getInformationAbotGroup(groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(
                        response->{
                            group=response;},
                        error->{
                            Log.e("ERROR",error+"");},
                        ()->{
                            groupView.stopLoading();
                            groupView.getInformationAboutGroup(group);});
    }
}
