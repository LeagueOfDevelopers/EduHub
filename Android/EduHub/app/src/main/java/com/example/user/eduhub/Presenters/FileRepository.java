package com.example.user.eduhub.Presenters;

import android.app.Activity;
import android.content.Context;
import android.database.Cursor;
import android.net.Uri;
import android.provider.MediaStore;
import android.util.Log;

import com.example.user.eduhub.Interfaces.Presenters.IFileRepository;
import com.example.user.eduhub.Interfaces.View.IFileRepositoryView;
import com.example.user.eduhub.Models.AddFileModel;
import com.example.user.eduhub.Models.AddFileResponseModel;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;
import com.google.gson.Gson;


import java.io.File;
import java.io.FileOutputStream;
import java.util.concurrent.TimeUnit;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;

/**
 * Created by User on 02.03.2018.
 */

public class FileRepository implements IFileRepository {
    IFileRepositoryView fileRepositoryView;
    ResponseBody result;

    AddFileResponseModel addFileResponseModel;
    private Activity activity;

    public FileRepository(IFileRepositoryView fileRepositoryView,Activity activity) {
        this.fileRepositoryView = fileRepositoryView;
        this.activity=activity;

    }
    EduHubApi eduHubApi= RetrofitBuilder.getApi();

    @Override
    public void loadFileToServer(String token, Uri uri) {
        AddFileModel addFileModel=new AddFileModel();
        File file=new File(getImagePath(uri));
        Log.d("FilePath",file.getPath());
        RequestBody requestFile=RequestBody.create(MediaType.parse("multipart/form-data"),file);
        MultipartBody.Part body=MultipartBody.Part.createFormData("file",file.getName(),requestFile);
        RequestBody description=RequestBody.create(MultipartBody.FORM,"TestFile");
        Gson gson=new Gson();
        gson.toJson(file);
        eduHubApi.loadFiletoServer("Bearer "+token,description,body)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{addFileResponseModel=next;},
                        throwable -> {
                            Log.e("FILEREP",throwable.toString());
                            Log.d("Message",throwable.getLocalizedMessage());},
                        ()->{fileRepositoryView.getResponse(addFileResponseModel);});

    }

    @Override
    public void loadFileFromServer(String token, String fileName) {
        eduHubApi.loafFileFromServer("Bearer "+token,fileName)

                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{
                    Log.d("getFile2",next.string());
                    result=next;
                },
                        throwable -> {Log.e("GetFile",throwable.toString());},
                        ()->{

                    fileRepositoryView.getFile(result);
                        });
    }
    public String getImagePath(Uri uri){
        Cursor cursor = activity.getContentResolver().query(uri, null, null, null, null);
        cursor.moveToFirst();
        String document_id = cursor.getString(0);
        document_id = document_id.substring(document_id.lastIndexOf(":")+1);
        cursor.close();

        cursor = activity.getContentResolver().query(
                android.provider.MediaStore.Images.Media.EXTERNAL_CONTENT_URI,
                null, MediaStore.Images.Media._ID + " = ? ", new String[]{document_id}, null);
        cursor.moveToFirst();
        String path = cursor.getString(cursor.getColumnIndex(MediaStore.Images.Media.DATA));
        cursor.close();

        return path;
    }

}

