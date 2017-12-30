package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.IAccountActivities;
import com.example.user.eduhub.Interfaces.ICallBack;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.RegistrationModel;
import com.example.user.eduhub.Models.RegistrationResponseModel;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by user on 16.12.2017.
 */

public class AccountActivities implements IAccountActivities {
    public AccountActivities(ICallBack callBack){
       this.callBack=callBack;
    }
    ICallBack callBack;
   String id;
   RegistrationModel registrationModel =new RegistrationModel();
   User user;
   LoginModel loginModel =new LoginModel();
    @Override
    public void UserLogin(String login, String password) {
        loginModel.setEmail(login);
        loginModel.setPassword(password);
        RetrofitBuilder.getApi().userLogin(loginModel).enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if(response.code()==200){
                    String token=response.body();
                    callBack.callBackLogin(token);
                }
                else {
                    callBack.callBackLoginError(response.code());
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

            }
        });

    }

    @Override
    public void UserRegistration(String login, String password, String name,Boolean isTeacher) {


        registrationModel.setEmail(login);
        registrationModel.setName(name);
        registrationModel.setPassword(password);
        registrationModel.setIsTeacher(isTeacher);
        RetrofitBuilder.getApi().userRegistration(registrationModel).enqueue(new Callback<RegistrationResponseModel>() {
            @Override
            public void onResponse(Call<RegistrationResponseModel> call, Response<RegistrationResponseModel> response) {
                if(response.code()==200){
                  id=response.body().toString();
                  callBack.callBackRegistrate(id);
                }else {
                    callBack.callBackRegistrationError(response.code());
                }

            }

            @Override
            public void onFailure(Call<RegistrationResponseModel> call, Throwable t) {

            }
        });

    }
}
