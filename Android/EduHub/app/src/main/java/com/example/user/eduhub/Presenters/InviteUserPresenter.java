package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Interfaces.Presenters.IInviteUserPresenter;
import com.example.user.eduhub.Interfaces.View.IInviteUserView;
import com.example.user.eduhub.Interfaces.View.ISearchResponse;
import com.example.user.eduhub.Models.InviteUserModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.UserProfile;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 31.01.2018.
 */

public class InviteUserPresenter implements IInviteUserPresenter ,ISearchResponse {
    IInviteUserView inviteUserView;
    SearchUserPresenter searchUserPresenter=new SearchUserPresenter(this);
    User user;
    String groupId;
    MemberRole role;

    public InviteUserPresenter(IInviteUserView iInviteUserView,String groupId,User user) {
        this.inviteUserView = iInviteUserView;
        this.groupId=groupId;
        this.user=user;
    }

    @Override
    public void inviteUser(String name, MemberRole role) {
        this.role=role;
        searchUserPresenter.searchUser(name);
    }

    @Override
    public void getResult(UserProfile userProfile) {
        InviteUserModel inviteUserModel=new InviteUserModel();
        inviteUserModel.setInvitedId(userProfile.getId());
        inviteUserModel.setRole(role.toString());
        Log.d("Role",inviteUserModel.getRole());
        Log.d("ID",inviteUserModel.getInvitedId());
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.invitedUser("Bearer "+user.getToken(),groupId,inviteUserModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(finish->{inviteUserView.getResponse();},
                        error->{
                    Log.e("ERRORORORO",error+"");
                    inviteUserView.getError(error);
                        });

    }

    @Override
    public void getError(Throwable error) {

    }
}
