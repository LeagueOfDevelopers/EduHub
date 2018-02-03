package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.IInvitationsPresenter;
import com.example.user.eduhub.Interfaces.View.IInvitationsView;
import com.example.user.eduhub.Models.Invitation;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 01.02.2018.
 */

public class InvitationsPresenter implements IInvitationsPresenter {
    IInvitationsView invitationsView;

    public InvitationsPresenter(IInvitationsView invitationsView) {
        this.invitationsView = invitationsView;
    }

    @Override
    public void loadInvitations(String token) {
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.getInvitations("Bearer "+token)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(result->{invitationsView.getInvitations((ArrayList<Invitation>) result.getInvitations());
                invitationsView.stopLoading();},
                        error->{
                            Log.e("ERROR_invitations",error+"");});
    }
}
