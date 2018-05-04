package ru.lod_misis.user.eduhub.Classes;

import java.util.ArrayList;

public class CheckLoading {
    private Boolean isLoading;
    private static CheckLoading instance;
    public CheckLoading(Boolean isLoading){
        this.isLoading=isLoading;
    }
    public static synchronized CheckLoading getInstance(){
        if(instance==null){
            instance=new CheckLoading(false);
        }
        return instance;
    }

    public Boolean getLoading() {
        return isLoading;
    }

    public void setLoading(Boolean loading) {
        isLoading = loading;
    }
}
