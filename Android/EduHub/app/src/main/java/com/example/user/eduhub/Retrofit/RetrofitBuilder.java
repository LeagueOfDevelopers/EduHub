package com.example.user.eduhub.Retrofit;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

/**
 * Created by user on 16.12.2017.
 */

public class RetrofitBuilder {
    static final String BASE_URL = "http://85.143.104.47:2411";

    public static EduHubApi getApi() {
        Gson gson = new GsonBuilder()
                .setLenient()
                .create();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(BASE_URL)
                .addConverterFactory(GsonConverterFactory.create(gson))
                .build();

        EduHubApi eduHubApi = retrofit.create(EduHubApi.class);
        return eduHubApi;

    }
}
