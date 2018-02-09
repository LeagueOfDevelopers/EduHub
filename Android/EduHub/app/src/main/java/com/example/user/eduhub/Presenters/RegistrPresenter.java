package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Fragments.LoginFragment;
import com.example.user.eduhub.Interfaces.Presenters.IRegistrPresenter;
import com.example.user.eduhub.Interfaces.View.IRegistrView;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 16.01.2018.
 */

public class RegistrPresenter implements IRegistrPresenter {
    private IRegistrView registrView;
    Disposable disposable;

    public RegistrPresenter(IRegistrView registrView) {
        this.registrView = registrView;
    }

    @Override
    public void RegistrationUser(String name, String email, String password, Boolean isTeacher,  String inviteCode) {
        if(!email.equals("")&&!password.equals("")&&!name.equals("")){

            RegistrationModel registrationModel=new RegistrationModel();
            registrationModel.setEmail(email);
            registrationModel.setName(name);
            registrationModel.setPassword(password);
            registrationModel.setIsTeacher(isTeacher);
            registrationModel.setInviteCode(inviteCode);
            EduHubApi eduHubApi= RetrofitBuilder.getApi();
            disposable=eduHubApi.userRegistration(registrationModel)
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(
                            next -> {
                            },
                            error -> {
                                registrView.getError(error);
                                Log.e("Throwavle",error.toString());
                            },
                            ()->{
                                LoginFragment fragment=new LoginFragment();
                                registrView.getResponse(fragment);}



                    );

        }else{


        }
    }
}
