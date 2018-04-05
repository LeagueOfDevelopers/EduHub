package ru.lod_misis.user.eduhub.Interfaces.View;

import java.util.ArrayList;

import ru.lod_misis.user.eduhub.Models.Group.Group;

/**
 * Created by User on 05.04.2018.
 */

public interface IFindGroupsView extends IBaseView{
    void getGroups(ArrayList<Group> groups);
}
