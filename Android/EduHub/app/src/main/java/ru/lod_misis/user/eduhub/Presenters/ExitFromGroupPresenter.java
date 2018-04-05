package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IExitFromGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IExitFromGroupView;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 17.02.2018.
 */

public class ExitFromGroupPresenter implements IExitFromGroupPresenter {
    IExitFromGroupView exitFromGroupView;
    EduHubApi eduHubApi= RetrofitBuilder.getApi();

    public ExitFromGroupPresenter(IExitFromGroupView exitFromGroupView) {
        this.exitFromGroupView = exitFromGroupView;
    }

    @Override
    public void exitFromGroupForUser(String token, String groupId, String memberId) {
        eduHubApi.exitFromGroup("Bearer "+token,groupId,memberId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{
            exitFromGroupView.getResponse();
                },throwable->{
                    Log.e("ExitExeption",throwable.toString());
                });
    }

    @Override
    public void exitFromGroupForTeacher(String token, String groupId) {
        eduHubApi.exitFromGroupForTeacher("Bearer "+token,groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{
                    exitFromGroupView.getResponse();
                },throwable->{
                    Log.e("ExitExeption",throwable.toString());
                });
    }
}
