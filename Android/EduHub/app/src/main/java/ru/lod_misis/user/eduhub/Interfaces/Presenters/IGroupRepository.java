package ru.lod_misis.user.eduhub.Interfaces.Presenters;

/**
 * Created by user on 21.12.2017.
 */

public interface IGroupRepository {

     void loadAllGroupsForUsers();
     void loadAllGroupsForTeachers();
     void loadUsersGroup(String token,String userId);
}
