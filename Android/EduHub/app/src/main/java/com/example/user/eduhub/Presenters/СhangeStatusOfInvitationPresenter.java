package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.IChangeStatusOfInvitationPresenter;
import com.example.user.eduhub.Interfaces.View.IChangeStatusOfInvitationView;
import com.example.user.eduhub.Models.ChangeInvitationStatusModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 02.02.2018.
 */

public class СhangeStatusOfInvitationPresenter implements IChangeStatusOfInvitationPresenter {
    IChangeStatusOfInvitationView changeStatusOfInvitationView;

    public СhangeStatusOfInvitationPresenter(IChangeStatusOfInvitationView changeStatusOfInvitationView) {
        this.changeStatusOfInvitationView = changeStatusOfInvitationView;
    }

    @Override
    public void changeStatus(String newStatus,String token,String inviteId) {
        ChangeInvitationStatusModel model=new ChangeInvitationStatusModel();
        model.setStatus(newStatus);
        model.setInvitationId(inviteId);
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.changeStatusOfInvitation("Bearer "+token,model)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(result->{
                            if(newStatus.equals("Accepted")){
                                changeStatusOfInvitationView.Possitive();
                            }
                            else {
                                changeStatusOfInvitationView.Negative();
                            }
                },
                        throwable -> {
                            Log.e("THROWABLE",throwable+"");});
    }
}
