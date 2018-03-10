package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.AddFileResponseModel;

import java.io.File;
import java.io.IOException;

import okhttp3.ResponseBody;

/**
 * Created by User on 02.03.2018.
 */

public interface IFileRepositoryView extends IBaseView {
    void getResponse(AddFileResponseModel addFileResponseModel);
    void getFile(ResponseBody file) throws IOException;
}
