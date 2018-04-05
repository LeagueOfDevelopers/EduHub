package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ICreateGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ICreateGroupView;
import ru.lod_misis.user.eduhub.Models.CreateGroupModel;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

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
    public void createGroup(String title, String description, ArrayList<String> tags, int size, Double cost, String groupType, Boolean isPrivate,String token) {
        createGroupModel.setDescription(description);
        Log.d("description",description);
        switch (groupType){
            case "Лекция":{
                createGroupModel.setGroupType(1);
                break;
            }
            case " Мастер класс":{
                createGroupModel.setGroupType(3);
                break;
            }
            case "Семинар":{
                createGroupModel.setGroupType(2);
                break;
            }

        }

        Log.d("NUMBER TYPE OFEDUCATION",groupType);
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
        EduHubApi eduHubApi= RetrofitBuilder.getApi();
        eduHubApi.createGroup("Bearer "+token,createGroupModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(done->{createGroupView.getResponse();},
                        error->{Log.e("exeption",error+"");});


    }
}
