package com.example.user.eduhub.Models;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import okhttp3.Headers;

/**
 * Created by User on 02.03.2018.
 */

public class AddFileResponseModel {
    @SerializedName("contentType")
    @Expose
    private String contentType;
    @SerializedName("contentDisposition")
    @Expose
    private String contentDisposition;
    @SerializedName("headers")
    @Expose
    private Headers headers;
    @SerializedName("length")
    @Expose
    private Integer length;
    @SerializedName("name")
    @Expose
    private String name;
    @SerializedName("fileName")
    @Expose
    private String fileName;

    public String getContentType() {
        return contentType;
    }

    public void setContentType(String contentType) {
        this.contentType = contentType;
    }

    public String getContentDisposition() {
        return contentDisposition;
    }

    public void setContentDisposition(String contentDisposition) {
        this.contentDisposition = contentDisposition;
    }

    public Headers getHeaders() {
        return headers;
    }

    public void setHeaders(Headers headers) {
        this.headers = headers;
    }

    public Integer getLength() {
        return length;
    }

    public void setLength(Integer length) {
        this.length = length;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getFileName() {
        return fileName;
    }

    public void setFileName(String fileName) {
        this.fileName = fileName;
    }
}
