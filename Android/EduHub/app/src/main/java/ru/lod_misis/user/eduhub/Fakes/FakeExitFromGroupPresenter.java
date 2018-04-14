package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.IExitFromGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IExitFromGroupView;

/**
 * Created by User on 17.02.2018.
 */

public class FakeExitFromGroupPresenter implements IExitFromGroupPresenter {
    IExitFromGroupView exitFromGroupView;

    public FakeExitFromGroupPresenter(IExitFromGroupView exitFromGroupView) {
        this.exitFromGroupView = exitFromGroupView;
    }

    @Override
    public void exitFromGroupForUser(String token, String groupId, String memberId, Context context) {
        exitFromGroupView.getResponse();
    }

    @Override
    public void exitFromGroupForTeacher(String token, String groupId, Context context) {
        exitFromGroupView.getResponse();
    }
}
