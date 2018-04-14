package ru.lod_misis.user.eduhub.Fakes;

import android.content.Context;

import ru.lod_misis.user.eduhub.Interfaces.Presenters.ICreateGroupPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.ICreateGroupView;

import java.util.ArrayList;

/**
 * Created by User on 27.01.2018.
 */

public class FakeCreateGroupPresenter implements ICreateGroupPresenter {
    ICreateGroupView createGroupView;

    public FakeCreateGroupPresenter(ICreateGroupView createGroupView) {
        this.createGroupView = createGroupView;
    }

    @Override
    public void createGroup(String title, String description, ArrayList<String> tags, int size, Double cost, String groupType, Boolean isPrivate, String token, Context context) {
        createGroupView.getResponse();
    }
}
