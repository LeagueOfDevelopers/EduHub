package ru.lod_misis.user.eduhub.Interfaces.View;

import ru.lod_misis.user.eduhub.Models.Group.Group;

/**
 * Created by User on 16.01.2018.
 */

public interface IGroupView extends IBaseView {
    void getInformationAboutGroup(Group group);
}
