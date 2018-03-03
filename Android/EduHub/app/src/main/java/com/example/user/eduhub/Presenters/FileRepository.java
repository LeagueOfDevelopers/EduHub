package com.example.user.eduhub.Presenters;

import android.app.Activity;
import android.database.Cursor;
import android.net.Uri;
import android.provider.MediaStore;
import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.IFileRepository;
import com.example.user.eduhub.Interfaces.View.IFileRepositoryView;
import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import org.apache.commons.io.FileUtils;

import java.io.File;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 02.03.2018.
 */

public class FileRepository implements IFileRepository {
    IFileRepositoryView fileRepositoryView;
    Activity activity;
    AddFileResponseModel addFileResponseModel;

    public FileRepository(IFileRepositoryView fileRepositoryView,Activity activity) {
        this.fileRepositoryView = fileRepositoryView;
        this.activity=activity;
    }
    EduHubApi eduHubApi= RetrofitBuilder.getApi();

    @Override
    public void loadFileToServer(String token, Uri uri) {

        File file= new File(getPath(uri));
        eduHubApi.loadFiletoServer("Bearer "+token,file)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{addFileResponseModel=next;},
                        throwable -> {
                            Log.e("FILEREP",throwable.toString());},
                        ()->{fileRepositoryView.getResponse(addFileResponseModel);});

    }

    @Override
    public void loadFileFromServer(String token, String fileName) {

    }
    public String getPath(Uri uri)
    {
        String[] projection = { MediaStore.Images.Media.DATA };
        Cursor cursor = activity.getContentResolver().query(uri, projection, null, null, null);
        if (cursor == null) return null;
        int column_index = cursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        String s=cursor.getString(column_index);
        cursor.close();
        return s;
    }
}
