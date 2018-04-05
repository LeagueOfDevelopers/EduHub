package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ILoginPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ILoginView;
import ru.lod_misis.user.eduhub.Models.LoginModel;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 16.01.2018.
 */

public class LoginPresenter implements ILoginPresenter {
    private ILoginView loginView;
    LoginModel loginModel;
    Disposable disposable;
    User user;

    public LoginPresenter(ILoginView loginView) {
        this.loginView = loginView;
    }

    @Override
    public void Login(String email, String password) {
        Log.d("emailLogin",email);
        if(!email.equals("")&&!password.equals("")){
            loginModel=new LoginModel();
            loginModel.setEmail(email);
            loginModel.setPassword(password);
            EduHubApi eduHubApi= RetrofitBuilder.getApi();
            disposable= eduHubApi.userLogin(loginModel)
                    .subscribeOn(Schedulers.io())
                    .observeOn(AndroidSchedulers.mainThread())
                    .subscribe(response->{
                                user=response;
                                loginView.login(user);},
                            error->{Log.e("LoginErr",error+"");
                        loginView.getError(error);});
        }
        else{

        }
    }
}
