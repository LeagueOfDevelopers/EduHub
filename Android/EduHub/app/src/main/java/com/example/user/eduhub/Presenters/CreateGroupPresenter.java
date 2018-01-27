package com.example.user.eduhub.Presenters;

import android.util.Log;

import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Interfaces.Presenters.ICreateGroupPresenter;
import com.example.user.eduhub.Interfaces.View.ICreateGroupView;
import com.example.user.eduhub.Models.CreateGroupModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 26.01.2018.
 */

public class CreateGroupPresenter implements ICreateGroupPresenter {
    ICreateGroupView createGroupView;
    CreateGroupModel createGroupModel=new CreateGroupModel();

    public CreateGroupPresenter(ICreateGroupView createGroupView) {
        this.createGroupView = createGroupView;
    }

    @Override
    public void createGroup(String title, String description, ArrayList<String> tags, int size, int cost, TypeOfEducation groupType, Boolean isPrivate,String token) {
        createGroupModel.setDescription(description);
        Log.d("description",description);
        createGroupModel.setGroupType(groupType.ordinal()+1);
        Log.d("NUMBER TYPE OFEDUCATION",(groupType.ordinal())+"");
        createGroupModel.setIsPrivate(isPrivate);
        Log.d("isPrivate",isPrivate.toString());
        createGroupModel.setMoneyPerUser(cost);
        Log.d("cost",cost+"");
        createGroupModel.setSize(size);
        Log.d("Size",size+"");
        createGroupModel.setTags(tags);
        Log.d("TAGS",tags.toString());
        createGroupModel.setTitle(title);
        Log.d("TITLE",title);
        EduHubApi eduHubApi=RetrofitBuilder.getApi();
        eduHubApi.createGroup("Bearer "+token,createGroupModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(done->{createGroupView.getResponse();},
                        error->{Log.e("exeption",error+"");});


    }
}
