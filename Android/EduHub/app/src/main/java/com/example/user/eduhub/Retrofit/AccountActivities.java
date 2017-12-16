package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.IAccountActivities;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.RegistrationModel;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by user on 16.12.2017.
 */

public class AccountActivities implements IAccountActivities {
   boolean  flag ;
   RegistrationModel model2=new RegistrationModel();
   User user;
   LoginModel model=new LoginModel();
    @Override
    public User UserLogin(String login, String password) {
        model.setEmail(login);
        model.setPassword(password);
        RetrofitBuilder.getApi().userLogin(model).enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if(response.code()==200){
                    user=new User();
                    user.setEmail(response.code()+"");
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

            }
        });
        return user;
    }

    @Override
    public Boolean UserRegistration(String login, String password, String name) {

        model2.setEmail(login);
        model2.setName(name);
        model2.setPassword(password);
        RetrofitBuilder.getApi().userRegistration(model2).enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if(response.code()==200){
                  flag =true;
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

            }
        });
        return flag;
    }
}
