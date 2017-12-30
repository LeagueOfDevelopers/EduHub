package com.example.user.eduhub.Retrofit;

import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.RegistrationModel;
import com.example.user.eduhub.Models.RegistrationResponseModel;
import com.google.gson.Gson;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.POST;

/**
 * Created by user on 16.12.2017.
 */

public interface EduHubApi {
    @POST("/api/account/registration")
    Call<RegistrationResponseModel> userRegistration(@Body RegistrationModel registrationModel);
    @POST("/api/account/login")
    Call<String> userLogin(@Body LoginModel loginModel);
}
