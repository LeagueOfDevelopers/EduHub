package com.example.user.eduhub.Fakes;

import com.example.user.eduhub.Interfaces.Presenters.IExitFromGroupPresenter;
import com.example.user.eduhub.Interfaces.View.IExitFromGroupView;

/**
 * Created by User on 17.02.2018.
 */

public class FakeExitFromGroupPresenter implements IExitFromGroupPresenter{
    IExitFromGroupView exitFromGroupView;

    public FakeExitFromGroupPresenter(IExitFromGroupView exitFromGroupView) {
        this.exitFromGroupView = exitFromGroupView;
    }

    @Override
    public void exitFromGroupForUser(String token, String groupId, String memberId) {
        exitFromGroupView.getResponse();
    }

    @Override
    public void exitFromGroupForTeacher(String token, String groupId) {
        exitFromGroupView.getResponse();
    }
}
