package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ICourseMethodsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ICourseMethodsView;
import ru.lod_misis.user.eduhub.Models.AddPlanModel;
import ru.lod_misis.user.eduhub.Models.UsersResponseModel;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 01.03.2018.
 */

public class CourseMethodsPresenter implements ICourseMethodsPresenter {
    private ICourseMethodsView addPlanForStudyView;
    EduHubApi eduHubApi= RetrofitBuilder.getApi();
    public CourseMethodsPresenter(ICourseMethodsView addPlanForStudyView) {
        this.addPlanForStudyView = addPlanForStudyView;
    }

    @Override
    public void addPlan(String token, String groupId,String path) {
        Log.d("FilePath2",path);
        AddPlanModel addPlanModel=new AddPlanModel();
        addPlanModel.setDescription(path);
        eduHubApi.addPlanForStudy("Bearer "+token,groupId,addPlanModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{
            addPlanForStudyView.getResponseAfterAddCourse();
                },
                        throwable -> {
                            Log.e("AddPlanPresenter",throwable.toString());});
    }

    @Override
    public void positiveResponse(String token, String groupId) {
        eduHubApi.positiveResponse("Bearer "+token,groupId)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{
                            addPlanForStudyView.getResponseAfterYourResponse();
                        },
                        throwable -> {Log.e("PositiveResponse",throwable.toString());});
    }

    @Override
    public void negativeResponse(String token, String groupId, String reason) {
        UsersResponseModel usersResponseModel=new UsersResponseModel();
        usersResponseModel.setReason(reason);
        eduHubApi.negativeResponse("Bearer "+token,groupId,usersResponseModel)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(()->{
            addPlanForStudyView.getResponseAfterYourResponse();
                },throwable -> {Log.e("NegativeUserResponse",throwable.toString());});
    }
}
