package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Classes.MemberRole;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IInviteUserPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IInviteUserView;
import ru.lod_misis.user.eduhub.Models.InviteUserModel;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 31.01.2018.
 */

public class InviteUserPresenter implements IInviteUserPresenter {
    IInviteUserView inviteUserView;

    User user;
    String groupId;
    MemberRole role;

    public InviteUserPresenter(IInviteUserView iInviteUserView) {
        this.inviteUserView = iInviteUserView;

    }

    @Override
    public void inviteUser(String userId, String role, String groupId, String token, Context context) {
        EduHubApi eduHubApi = RetrofitBuilder.getApi(context);
        InviteUserModel inviteUserModel = new InviteUserModel();
        inviteUserModel.setInvitedId(userId);
        Log.d("ROLELE",role);
        if(role.toString().equals("Ученик")){

            eduHubApi.invitedUser("Bearer "+token, groupId, inviteUserModel)
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(() -> {
                                inviteUserView.getResponse();
                            },
                            error -> {
                                Log.e("ERRORORORO", error + "");
                                inviteUserView.getError(error);
                            });
        }else{

            if(role.toString().equals("Учитель")){

                eduHubApi.invitedTeacher("Bearer "+token, groupId, inviteUserModel)
                        .subscribeOn(Schedulers.io())
                        .observeOn(AndroidSchedulers.mainThread())
                        .subscribe(() -> {
                                    inviteUserView.getResponse();
                                },
                                error -> {
                                    Log.e("ERRORORORO", error + "");
                                    inviteUserView.getError(error);
                                });
            }

        }



    }

}
