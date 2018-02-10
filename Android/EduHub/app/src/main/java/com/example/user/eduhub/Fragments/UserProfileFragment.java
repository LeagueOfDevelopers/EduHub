package com.example.user.eduhub.Fragments;

import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.example.user.eduhub.Dialog.CreateDialog;
import com.example.user.eduhub.Main2Activity;
import com.example.user.eduhub.Models.UserProfile.UserProfile;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.R;
import com.example.user.eduhub.RefactorProfile;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 26.01.2018.
 */

public class UserProfileFragment extends Fragment {
    private UserProfileResponse userProfile;
    CreateDialog createDialog;
    DialogInterface.OnClickListener myClickListener;
    SharedPreferences sharedPreferences;
    public void setUserProfile(UserProfileResponse userProfile) {
        this.userProfile = userProfile;
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.user_profile, null);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Мой профиль");
        TextView userName=v.findViewById(R.id.name_user_profile);
        TextView userEmail=v.findViewById(R.id.email_user_profile);
        TextView userName2=v.findViewById(R.id.name_user_profile2);
        TextView userEmail2=v.findViewById(R.id.email_user_profile2);
        TextView sex=v.findViewById(R.id.sex);
        TextView birthYear=v.findViewById(R.id.birth_year);
        TextView aboutMe=v.findViewById(R.id.aboutMe);
        Button exit=v.findViewById(R.id.exit_user);
        ImageView refactor=v.findViewById(R.id.refactor);
        sharedPreferences=getActivity().getSharedPreferences("User",MODE_PRIVATE);

        userEmail.setText(userProfile.getUserProfile().getEmail());
        userEmail2.setText(userProfile.getUserProfile().getEmail());
        userName.setText(userProfile.getUserProfile().getName());
        userName2.setText(userProfile.getUserProfile().getName());

        if(userProfile.getUserProfile().getIsMan()){
            sex.setText("Мужской");
        }else {
            sex.setText("Женский");
        }
        if(userProfile.getUserProfile().getBirthYear().equals("")){
            v.findViewById(R.id.card_of_birth).setVisibility(View.GONE);
        }else {
            birthYear.setText(userProfile.getUserProfile().getBirthYear());
        }


        if(userProfile.getUserProfile().getAboutUser()==null){
            v.findViewById(R.id.card_of_aboutMe).setVisibility(View.GONE);
        }else{
            aboutMe.setText(userProfile.getUserProfile().getAboutUser());}


        myClickListener = new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                switch (which) {
                    // положительная кнопка
                    case Dialog.BUTTON_POSITIVE:
                        Intent intent=new Intent(getActivity(), Main2Activity.class);
                        SharedPreferences.Editor editor=sharedPreferences.edit();
                        editor.clear();
                        editor.commit();
                        getActivity().startActivity(intent);

                        break;
                    // негативная кнопка
                    case Dialog.BUTTON_NEGATIVE:

                        break;

                }
            }
        };
        exit.setOnClickListener(click->{
            createDialog=new CreateDialog(getContext(),myClickListener);
            createDialog.onCreateDialog(2).show();

        });
        refactor.setOnClickListener(click->{
            Intent intent=new Intent(getActivity(),RefactorProfile.class);
            intent.putExtra("User",userProfile);
            getActivity().startActivity(intent);
        });

        return v;
    }
}
