package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

import java.util.ArrayList;

/**
 * Created by User on 16.02.2018.
 */

public interface IChangeUsersDataPresenter {
    void changeUsersData(String token,String name, String aboutUser, ArrayList<String> contacts,Integer birthYear,String avatarLink,String sex,Boolean isTeacher,String[] skills, Context context);
}
