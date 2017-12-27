package com.example.user.eduhub.Retrofit;

import android.util.Log;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.IAccountActivities;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.RegistrationModel;
import com.example.user.eduhub.Models.RegistrationResponseModel;
import com.google.gson.Gson;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by user on 16.12.2017.
 */

public class AccountActivities implements IAccountActivities {
   boolean  flag ;
   RegistrationModel registrationModel =new RegistrationModel();
   User user;
   LoginModel loginModel =new LoginModel();
    @Override
    public User UserLogin(String login, String password) {
        loginModel.setEmail(login);
        loginModel.setPassword(password);
        RetrofitBuilder.getApi().userLogin(loginModel).enqueue(new Callback<RegistrationResponseModel>() {
            @Override
            public void onResponse(Call<RegistrationResponseModel> call, Response<RegistrationResponseModel> response) {
                if(response.code()==200){
                    user=new User();
                    user.setEmail(response.code()+"");
                }

            }

            @Override
            public void onFailure(Call<RegistrationResponseModel> call, Throwable t) {

            }
        });
        return user;
    }

    @Override
    public Boolean UserRegistration(String login, String password, String name,Boolean isTeacher) {

        flag=false;
        registrationModel.setEmail(login);
        registrationModel.setName(name);
        registrationModel.setPassword(password);
        registrationModel.setIsTeacher(isTeacher);
        RetrofitBuilder.getApi().userRegistration(registrationModel).enqueue(new Callback<RegistrationResponseModel>() {
            @Override
            public void onResponse(Call<RegistrationResponseModel> call, Response<RegistrationResponseModel> response) {
                if(response.code()==200){
                  flag =true;
                }

            }

            @Override
            public void onFailure(Call<RegistrationResponseModel> call, Throwable t) {
                
            }
        });
        return flag;
    }
}
