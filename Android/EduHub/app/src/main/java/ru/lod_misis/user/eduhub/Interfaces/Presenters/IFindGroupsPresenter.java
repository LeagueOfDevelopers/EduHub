package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

import java.util.ArrayList;

/**
 * Created by User on 05.04.2018.
 */

public interface IFindGroupsPresenter {
    void findGroupsWithoutFilters(String title, Context context);
    void findGroupsWithFilters(Double minPrice, Double maxPrice, String title,
                               ArrayList<String> tags,String type,Boolean formed,Context context);
}
