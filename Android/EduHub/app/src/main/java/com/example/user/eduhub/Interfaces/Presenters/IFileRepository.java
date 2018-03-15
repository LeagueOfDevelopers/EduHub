package com.example.user.eduhub.Interfaces.Presenters;

import android.net.Uri;

import java.net.URI;

/**
 * Created by User on 02.03.2018.
 */

public interface IFileRepository {
    void loadImageToServer(String token, Uri uri);
    void loadFileFromServer(String token,String fileName);
    void loadFiletoServer(String token, Uri uri);
}
