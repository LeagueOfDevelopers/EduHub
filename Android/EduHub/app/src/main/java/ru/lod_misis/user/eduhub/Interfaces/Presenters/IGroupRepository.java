package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by user on 21.12.2017.
 */

public interface IGroupRepository {

     void loadAllGroupsForUsers( Context context);
     void loadAllGroupsForTeachers( Context context);
     void loadUsersGroup(String token,String userId, Context context);
}
