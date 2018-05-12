package ru.lod_misis.user.eduhub.Presenters;

import android.content.Context;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.schedulers.Schedulers;
import ru.lod_misis.user.eduhub.Interfaces.Presenters.IFindTagPresenter;
import ru.lod_misis.user.eduhub.Interfaces.View.IFindTagsView;
import ru.lod_misis.user.eduhub.Models.Tag;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;
import ru.lod_misis.user.eduhub.Retrofit.RetrofitBuilder;

public class FindTagsPresenter implements IFindTagPresenter {
    IFindTagsView findTagsView;
    EduHubApi eduHubApi;
    public FindTagsPresenter(IFindTagsView findTagsView, Context context) {
        this.findTagsView = findTagsView;
        eduHubApi= RetrofitBuilder.getApi(context);
    }

    @Override
    public void findTags(String tag) {
        Tag tag1=new Tag();
        tag1.setTag(tag);
        eduHubApi.findTags(tag)
                .subscribeOn(Schedulers.io())
                .observeOn(AndroidSchedulers.mainThread())
                .subscribe(tags->{
                            ArrayList<String> finakTags=new ArrayList<>();
                            for (Tag newTag :tags) {
                                finakTags.add(newTag.getTag());
                            }
                            findTagsView.getTags(finakTags);},
                        throwable -> {});
    }
}
