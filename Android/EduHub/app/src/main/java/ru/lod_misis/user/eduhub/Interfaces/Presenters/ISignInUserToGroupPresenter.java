package ru.lod_misis.user.eduhub.Interfaces.Presenters;

/**
 * Created by User on 01.02.2018.
 */

public interface ISignInUserToGroupPresenter {
    void signInUserToGroup(String token,String groupId);
    void signInTeacherToGroup(String token,String groupId);
}