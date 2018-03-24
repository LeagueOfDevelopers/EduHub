package com.example.user.eduhub.Interfaces.Presenters;

/**
 * Created by User on 17.02.2018.
 */

public interface IExitFromGroupPresenter {
    void exitFromGroupForUser(String token,String groupId,String memberId);
    void exitFromGroupForTeacher(String token,String groupId);
}
