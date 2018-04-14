package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 02.02.2018.
 */

public interface IChangeStatusOfInvitationPresenter {
    void changeStatus(String newStatus,String token,String inviteId, Context context);
}
