package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 27.02.2018.
 */

public interface IRefreshTokenPresenter {
    void refreshToken(String token, Context context);
}
