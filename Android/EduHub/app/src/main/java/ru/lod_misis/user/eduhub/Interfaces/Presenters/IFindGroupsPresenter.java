package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import java.util.ArrayList;

/**
 * Created by User on 05.04.2018.
 */

public interface IFindGroupsPresenter {
    void findGroupsWithoutFilters(String title);
    void findGroupsWithFilters(double minPrice, double maxPrice, String title,
                               ArrayList<String> tags,String type,Boolean formed);
}
