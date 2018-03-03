package com.example.user.eduhub.Interfaces.Presenters;

import android.net.Uri;

import java.net.URI;

/**
 * Created by User on 02.03.2018.
 */

public interface IFileRepository {
    void loadFileToServer(String token, Uri uri);
    void loadFileFromServer(String token,String fileName);
}
