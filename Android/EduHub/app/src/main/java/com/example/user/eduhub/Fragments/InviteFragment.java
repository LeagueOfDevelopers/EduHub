package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by User on 12.01.2018.
 */

public class InviteFragment extends Fragment {
    private String groupId;
    private User user;
    private MainGroupFragment mainGroupFragment;
    IFragmentsActivities fragmentsActivities;

    public void setMainGroupFragment(MainGroupFragment mainGroupFragment) {
        this.mainGroupFragment = mainGroupFragment;
    }

    public void setUser(User user) {
        this.user=user;
    }

    public void setGroupId(String groupId) {

        this.groupId = groupId;
    }

    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        Disposable disposable;
        View v = inflater.inflate(R.layout.invite_user_fragment, null);
        EditText edit=v.findViewById(R.id.id_invited);
        Button button= v.findViewById(R.id.invite_button);
        button.setOnClickListener(click->{
            if(!edit.getText().toString().equals("")){
                EduHubApi eduHubApi= RetrofitBuilder.getApi();
                eduHubApi.invitedUser(user.getToken(),groupId,user.getUserId(),edit.getText().toString())
                        .subscribeOn(Schedulers.io())
                        .observeOn(AndroidSchedulers.mainThread())
                        .subscribe(next->{},
                                error->{
                                    Log.e("ERROR",error+"");},
                                ()->{fragmentsActivities.switchingFragmets(mainGroupFragment);});

            }
        });

        return v;
    }


}
