package ru.lod_misis.user.eduhub.Interfaces.View;

/**
 * Created by User on 16.01.2018.
 */

public interface IBaseView {
    void showLoading();
    void stopLoading();
    void getError(Throwable error);
}
