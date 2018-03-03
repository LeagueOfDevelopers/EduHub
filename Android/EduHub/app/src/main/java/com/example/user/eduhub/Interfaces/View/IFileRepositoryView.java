package com.example.user.eduhub.Interfaces.View;

import com.example.user.eduhub.Models.AddFileResponseModel;

import java.io.File;

/**
 * Created by User on 02.03.2018.
 */

public interface IFileRepositoryView extends IBaseView {
    void getResponse(AddFileResponseModel addFileResponseModel);
}
