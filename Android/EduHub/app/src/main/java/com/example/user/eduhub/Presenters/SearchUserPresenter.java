package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.ISearchUserPresenter;
import com.example.user.eduhub.Interfaces.View.ISearchResponse;
import com.example.user.eduhub.Models.SearchModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 31.01.2018.
 */

public class SearchUserPresenter implements ISearchUserPresenter {
    ISearchResponse searchResponse;

    public SearchUserPresenter(ISearchResponse searchResponse) {
        this.searchResponse = searchResponse;
    }

    @Override
    public void searchUser(String name) {
        Log.d("NAME",name);
        SearchModel searchModel=new SearchModel();
        searchModel.setName(name);
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.searchUser(searchModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(finish->{
                    searchResponse.getResult(finish.getUsers());},
                        error->{
                            Log.e("ERROR",error+" ");});
    }

    @Override
    public void searchUserForInvitation(String name, String groupId) {
        Log.d("NAME",name);
        SearchModel searchModel=new SearchModel();
        searchModel.setUsername(name);
        searchModel.setGroupId(groupId);
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.searchUserForInvitation(searchModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(finish->{
                            searchResponse.getResult(finish.getUsers());},
                        error->{
                            Log.e("ERROR",error+" ");});
    }
}
