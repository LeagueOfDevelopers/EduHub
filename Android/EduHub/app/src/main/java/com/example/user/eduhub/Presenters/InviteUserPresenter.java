package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Interfaces.Presenters.IInviteUserPresenter;
import com.example.user.eduhub.Interfaces.View.IInviteUserView;
import com.example.user.eduhub.Interfaces.View.ISearchResponse;
import com.example.user.eduhub.Models.InviteUserModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void inviteUser(String userId, String role,String groupId,String token) {
        EduHubApi eduHubApi = RetrofitBuilder.getApi();
        InviteUserModel inviteUserModel = new InviteUserModel();
        inviteUserModel.setInvitedId(userId);

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
