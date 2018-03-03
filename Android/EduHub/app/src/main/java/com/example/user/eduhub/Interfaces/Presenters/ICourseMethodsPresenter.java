package com.example.user.eduhub.Interfaces.Presenters;

/**
 * Created by User on 01.03.2018.
 */

public interface ICourseMethodsPresenter {
    void addPlan(String token,String groupId,String path);
    void positiveResponse(String token,String groupId);
    void negativeResponse(String token,String groupId,String reason);
}
