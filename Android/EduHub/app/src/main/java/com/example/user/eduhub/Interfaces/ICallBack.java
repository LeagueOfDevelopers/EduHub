package com.example.user.eduhub.Interfaces;

/**
 * Created by User on 28.12.2017.
 */

public interface ICallBack {
    void callBackRegistrate(String id);
    void callBackRegistrationError(int code);
    void callBackLogin(String token);
    void callBackLoginError(int code);
}
