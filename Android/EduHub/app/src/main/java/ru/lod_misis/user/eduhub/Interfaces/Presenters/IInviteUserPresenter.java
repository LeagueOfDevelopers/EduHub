package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.content.Context;

/**
 * Created by User on 31.01.2018.
 */

public interface IInviteUserPresenter {
    void inviteUser(String userId, String role,String groupId,String token, Context context);
}

