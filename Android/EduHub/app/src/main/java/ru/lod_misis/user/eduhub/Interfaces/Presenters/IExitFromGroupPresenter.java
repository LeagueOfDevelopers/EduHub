package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 17.02.2018.
 */

public interface IExitFromGroupPresenter {
    void exitFromGroupForUser(String token,String groupId,String memberId, Context context);
    void exitFromGroupForTeacher(String token,String groupId, Context context);
}
