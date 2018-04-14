package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 01.03.2018.
 */

public interface ICourseMethodsPresenter {
    void addPlan(String token,String groupId,String path, Context context);
    void positiveResponse(String token,String groupId, Context context);
    void negativeResponse(String token,String groupId,String reason, Context context);
}
