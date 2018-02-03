package com.example.user.eduhub.Fragments;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.SharedPreferences;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.annotation.RequiresApi;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;


import com.example.user.eduhub.Classes.MemberRole;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Interfaces.Presenters.IInviteUserPresenter;
import com.example.user.eduhub.Interfaces.View.IInviteUserView;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.InviteUserPresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 12.01.2018.
 */

public class InviteFragment extends Fragment implements IInviteUserView {
    private String groupId;
    private User user;
    private Toolbar toolbar;
    private InviteUserPresenter inviteUserPresenter;
    IFragmentsActivities fragmentsActivities;
    SavedDataRepository savedDataRepository=new SavedDataRepository();


    public void setGroupId(String groupId) {

        this.groupId = groupId;
    }

    public void setToolbar(Toolbar toolbar) {
        this.toolbar = toolbar;
    }


    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {


        View v = inflater.inflate(R.layout.invite_user_fragment, null);
        SharedPreferences sPref=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user= savedDataRepository.loadSavedData(sPref);
        inviteUserPresenter=new InviteUserPresenter(this,groupId,user);
        toolbar.setTitle("Приглашение пользователя");
        EditText edit=v.findViewById(R.id.id_invited);
        Button button= v.findViewById(R.id.invite_button);
        button.setOnClickListener(click->{
            if(!edit.getText().toString().equals("")){
                inviteUserPresenter.inviteUser(edit.getText().toString(), MemberRole.Teacher);

            }else{

            }
        });

        return v;
    }


    @Override
    public void showLoading() {

    }

    @Override
    public void stopLoading() {

    }

    @Override
    public void getError(Throwable error) {

    }

    @Override
    public void getResponse() {
        MainGroupFragment mainGroupFragment=new MainGroupFragment();
        fragmentsActivities.switchingFragmets(mainGroupFragment);
        MakeToast("Пользователь приглашен");
    }
    private void MakeToast(String s){
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }
}
