package ru.lod_misis.user.eduhub.Presenters;

import android.util.Log;

import java.util.ArrayList;
import java.util.List;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IFindGroupsPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IFindGroupsView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

/**
 * Created by User on 05.04.2018.
 */

public class FIndGroupsPresenter implements IFindGroupsPresenter {
    private IFindGroupsView findGroupsView;
    EduHubApi eduHubApi= RetrofitBuilder.getApi();
    ArrayList<Group> groups;
    public FIndGroupsPresenter(IFindGroupsView findGroupsView) {
        this.findGroupsView = findGroupsView;
    }

    @Override
    public void findGroupsWithoutFilters(String title) {
        eduHubApi.findGroupsWithOutFilters(title)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(next->{groups=(ArrayList<Group>) next;},
                        throwable -> {
                            Log.e("FindGroupsErr",throwable.toString());},
                        ()->{findGroupsView.getGroups(groups);});
    }

    @Override
    public void findGroupsWithFilters(double minPrice, double maxPrice, String title, ArrayList<String> tags, String type, Boolean formed) {

    }
}
