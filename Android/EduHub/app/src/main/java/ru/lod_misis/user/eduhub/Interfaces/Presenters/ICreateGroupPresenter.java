package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

import java.util.ArrayList;

/**
 * Created by User on 26.01.2018.
 */

public interface ICreateGroupPresenter {
    void createGroup(String title, String description, ArrayList<String> tags, int size, Double cost, String groupType,Boolean isPrivate,String token, Context context);
}
