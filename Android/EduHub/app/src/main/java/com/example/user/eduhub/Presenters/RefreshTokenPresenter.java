package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.IRefreshTokenPresenter;
import com.example.user.eduhub.Interfaces.View.IRefreshTokenView;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 27.02.2018.
 */

public class RefreshTokenPresenter implements IRefreshTokenPresenter {
    private IRefreshTokenView refreshTokenView;

    public RefreshTokenPresenter(IRefreshTokenView refreshTokenView) {
        this.refreshTokenView = refreshTokenView;
    }

    @Override
    public void refreshToken(String token) {
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.refreshToken("Bearer "+token)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(finish->{refreshTokenView.getResponse(finish);},
                        throwable -> {
                            Log.e("errRefreshToken",throwable.toString());
                    refreshTokenView.getError(throwable);});

    }
}
