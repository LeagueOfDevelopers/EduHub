package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.User;

/**
 * Created by User on 27.02.2018.
 */

public interface IRefreshTokenView extends IBaseView {
    void getResponse(User user);
    void getThrowable();
}
