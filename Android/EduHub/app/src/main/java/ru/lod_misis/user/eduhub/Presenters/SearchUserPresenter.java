package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;
import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ISearchUserPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ISearchResponse;
import ru.lod_misis.user.eduhub.Models.SearchModel;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void searchUser(String name, Context context) {
        Log.d("NAME",name);
        SearchModel searchModel=new SearchModel();
        searchModel.setName(name);
        EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
        eduHubApi.searchUser(searchModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(finish->{
                    searchResponse.getResult(finish.getUsers());},
                        error->{
                            Log.e("ERROR",error+" ");});
    }

    @Override
    public void searchUserForInvitation(String name, String groupId,Context context) {
        Log.d("NAME",name);
        SearchModel searchModel=new SearchModel();
        searchModel.setUsername(name);
        searchModel.setGroupId(groupId);
        EduHubApi eduHubApi= RetrofitBuilder.getApi(context);
        eduHubApi.searchUserForInvitation(searchModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(finish->{
                            searchResponse.getResult(finish.getUsers());},
                        error->{
                            Log.e("ERROR",error+" ");});
    }
}
