package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.INotificationsSettingsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.INotificationsSettingsView;
import ru.lod_misis.user.eduhub.Models.UserProfile.ChangeSettingNotification;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

public class NotificationsSettingsPresenter implements INotificationsSettingsPresenter {
    INotificationsSettingsView notificationsSettingsView;
    EduHubApi eduHubApi;
    public NotificationsSettingsPresenter(INotificationsSettingsView notificationsSettingsView, Context context) {
        this.notificationsSettingsView = notificationsSettingsView;
         eduHubApi= RetrofitBuilder.getApi(context);
    }

    @Override
    public void getSettings(String token) {
        eduHubApi.getSettingsNotifications("Bearer "+token)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(settings -> {notificationsSettingsView.getSettings(settings);},
                        throwable -> {});
    }

    @Override
    public void changeSettings(String token, ChangeSettingNotification model) {
        eduHubApi.changeNotificationsSettings("Bearer "+token,model)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe();

    }
}
