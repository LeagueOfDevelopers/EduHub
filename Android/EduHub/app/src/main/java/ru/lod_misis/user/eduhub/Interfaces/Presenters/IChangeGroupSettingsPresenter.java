package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

import java.util.ArrayList;

/**
 * Created by User on 24.02.2018.
 */

public interface IChangeGroupSettingsPresenter {
    void chngeGroupSettings(String token, String groupId, String groupName, Integer size, Double cost, String typeOfEducation, ArrayList<String> tags, String description, Boolean isPrivate, Context context);
}
