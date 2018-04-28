package ru.lod_misis.user.eduhub.Interfaces.Presenters;

import android.net.Uri;

/**
 * Created by User on 02.03.2018.
 */

public interface IFileRepository {
    void loadImageToServer(String token, Uri uri);
    void loadFileFromServer(String token,String fileName);
    void loadImageFromServer(String token,String fileName);
    void loadFiletoServer(String token, Uri uri);
}
