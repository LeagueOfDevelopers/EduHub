package com.example.user.eduhub.Fragments;

import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.example.user.eduhub.Fakes.FakeUserProfilePresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.IUserProfileView;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.UserProfile;
import com.example.user.eduhub.Presenters.UserProfilePresenter;
import com.example.user.eduhub.R;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 30.01.2018.
 */

public class ProfileTransactionFragment extends Fragment implements IUserProfileView {
    FragmentTransaction fragmentTransaction;
    UserProfilePresenter userProfilePresenter=new UserProfilePresenter(this);
    FakesButton fakesButton=new FakesButton();
    SharedPreferences sPref;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakeUserProfilePresenter fakeUserProfilePresenter=new FakeUserProfilePresenter(this);

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.conteiner_user_profile, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мой профиль");
        sPref=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        User user=savedDataRepository.loadSavedData(sPref);
        Log.d("TOKEN",user.getToken());
        Log.d("CHECK BUTTON",fakesButton.getCheckButton().toString());
        if(!fakesButton.getCheckButton()){

            userProfilePresenter.loadUserProfile(user.getToken());}
        else{
            fakeUserProfilePresenter.loadUserProfile(user.getToken());
        }
    return v;}



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
    public void getUserProfile(UserProfile userProfile) {
        fragmentTransaction=getFragmentManager().beginTransaction();
        Log.d("USER ROLE",userProfile.getIsTeacher().toString());
        if(userProfile.getIsTeacher()){
            TeacherProfile teacherProfile=new TeacherProfile();
            teacherProfile.setUserProfile(userProfile);
            fragmentTransaction.replace(R.id.UserFragmentsConteiner,teacherProfile);
            fragmentTransaction.commit();
        }
        else{
            UserProfileFragment userProfile1=new UserProfileFragment();
            userProfile1.setUserProfile(userProfile);
            fragmentTransaction.replace(R.id.UserFragmentsConteiner,userProfile1);
            fragmentTransaction.commit();
        }
    }
}
