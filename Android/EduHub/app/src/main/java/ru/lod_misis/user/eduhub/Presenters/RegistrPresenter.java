package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Fragments.LoginFragment;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IRegistrPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IRegistrView;
import ru.lod_misis.user.eduhub.Models.Registration.RegistrationModel2;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void RegistrationUser(String name, String email, String password, Boolean isTeacher, Context context) {
        if(!email.equals("")&&!password.equals("")&&!name.equals("")){

                RegistrationModel2 registrationModel=new RegistrationModel2();
                registrationModel.setEmail(email);
                registrationModel.setName(name);
                registrationModel.setPassword(password);
                registrationModel.setIsTeacher(isTeacher);
                Log.d("email",email);
            Log.d("name",name);
            Log.d("password",password);
            Log.d("isTeacher",isTeacher.toString());
                EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
                disposable=eduHubApi.userRegistrationWithoutInviteCode(registrationModel)
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
