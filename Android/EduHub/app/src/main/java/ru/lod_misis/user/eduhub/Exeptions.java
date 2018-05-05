package ru.lod_misis.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;

import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;

import ru.lod_misis.user.eduhub.Interfaces.IExeptions;

public class Exeptions implements IExeptions {
    @Override
    public String parseExeption(Throwable throwable) {
        String resultMessage="";
        if(throwable instanceof HttpException){
            switch (((HttpException) throwable).code()){
                case 401:{resultMessage="";
                return resultMessage;}
                case 400:{return resultMessage;}
                case 404:{return resultMessage;}
            }
        }
        return null;
    }
}
