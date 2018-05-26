package ru.lod_misis.user.eduhub.Presenters;


import android.content.Context;
import android.util.Log;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import retrofit2.converter.gson.GsonConverterFactory;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.INotificationsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.INotificationsView;
import ru.lod_misis.user.eduhub.Models.Notivications.ConvertNotifications;
import ru.lod_misis.user.eduhub.Models.Notivications.CourseAccepted;
import ru.lod_misis.user.eduhub.Models.Notivications.CourseDeclined;
import ru.lod_misis.user.eduhub.Models.Notivications.CourseFinished;
import ru.lod_misis.user.eduhub.Models.Notivications.CourseSuggested;
import ru.lod_misis.user.eduhub.Models.Notivications.GroupFormed;
import ru.lod_misis.user.eduhub.Models.Notivications.Invitation;
import ru.lod_misis.user.eduhub.Models.Notivications.InvitationAccepted;
import ru.lod_misis.user.eduhub.Models.Notivications.InvitationDeclined;
import ru.lod_misis.user.eduhub.Models.Notivications.MemberLeft;
import ru.lod_misis.user.eduhub.Models.Notivications.NewCreator;
import ru.lod_misis.user.eduhub.Models.Notivications.NewMember;
import ru.lod_misis.user.eduhub.Models.Notivications.NewReview;
import ru.lod_misis.user.eduhub.Models.Notivications.Notification;
import ru.lod_misis.user.eduhub.Models.Notivications.Notifications;
import ru.lod_misis.user.eduhub.Models.Notivications.ReportMessage;
import ru.lod_misis.user.eduhub.Models.Notivications.SunctionApplied;
import ru.lod_misis.user.eduhub.Models.Notivications.TeacherFound;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.Date;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 01.02.2018.
 */

public class NotificationsPresenter implements INotificationsPresenter {
    INotificationsView invitationsView;
    ArrayList<Notifications> notifications;
    Notification newNot;
    ArrayList<Notification> notifications2=new ArrayList<>();
    ConvertNotifications convertNot=new ConvertNotifications();


    public NotificationsPresenter(INotificationsView invitationsView) {
        this.invitationsView = invitationsView;
    }

    @Override
    public void loadInvitations(String token, Context context) {
        EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
        eduHubApi.getInvitations("Bearer "+token)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(result->{invitationsView.getInvitations((ArrayList<Invitation>) result.getInvitations());
                invitationsView.stopLoading();},
                        error->{
                            Log.e("ERROR_invitations",error+"");});
    }

    @Override
    public void getAllNotifications(String token,Context context) {
notifications2=new ArrayList<>();
Log.d("token",token);
        EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
        eduHubApi.loadAllNotifications("Bearer "+token)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{notifications=(ArrayList<Notifications>)next;},
                        throwable -> {Log.e("Notif",throwable.toString());},
                        ()->{
                            for (Notifications notification:notifications
                                 ) {
                                newNot=convertNot.convertCommonNotificationToNotofication(notification);
                                if(newNot!=null){
                                    notifications2.add(newNot);
                                }

                            }
                            invitationsView.getAllNotifications(notifications2);
                        });
    }

}
