package com.example.user.eduhub.Presenters;

import com.example.user.eduhub.Interfaces.Presenters.IExitFromGroupPresenter;
import com.example.user.eduhub.Interfaces.View.IExitFromGroupView;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

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
                });
    }

    @Override
    public void exitFromGroupForTeacher(String token, String groupId, String memberId) {
        eduHubApi.exitFromGroupForTeacher("Bearer "+token,groupId,memberId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{
                    exitFromGroupView.getResponse();
                });
    }
}
