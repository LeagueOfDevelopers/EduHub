package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IRefreshTokenPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IRefreshTokenView;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
