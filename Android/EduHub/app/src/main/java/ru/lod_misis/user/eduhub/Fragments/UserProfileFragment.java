package ru.lod_misis.user.eduhub.Fragments;

import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.StaggeredGridLayoutManager;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Adapters.Contacts_adapter_profile;
import ru.lod_misis.user.eduhub.Dialog.CreateDialog;
import ru.lod_misis.user.eduhub.Main2Activity;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.R;
import ru.lod_misis.user.eduhub.RefactorProfile;

import java.util.ArrayList;

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
        RecyclerView contacts=v.findViewById(R.id.contacts);

        sharedPreferences=getActivity().getSharedPreferences("User",MODE_PRIVATE);

        userEmail.setText(userProfile.getUserProfile().getEmail());
        userEmail2.setText(userProfile.getUserProfile().getEmail());
        userName.setText(userProfile.getUserProfile().getName());
        userName2.setText(userProfile.getUserProfile().getName());

        if(!userProfile.getUserProfile().getGender().equals("0")){
            if (userProfile.getUserProfile().getGender().equals("1")){
                sex.setText("Мужской");
            }else{
                sex.setText("Женский");
            }
            v.findViewById(R.id.card_of_sex).setVisibility(View.VISIBLE);
        }else {
            v.findViewById(R.id.card_of_sex).setVisibility(View.GONE);
        }
        if(userProfile.getUserProfile().getBirthYear().toString().equals("0")){
            v.findViewById(R.id.card_of_birth).setVisibility(View.GONE);
        }else {
            birthYear.setText(userProfile.getUserProfile().getBirthYear().toString());
            v.findViewById(R.id.card_of_birth).setVisibility(View.VISIBLE);
        }
        if(userProfile.getUserProfile().getContacts()==null){
            v.findViewById(R.id.links).setVisibility(View.GONE);
        }else{
            contacts.setHasFixedSize(true);
            Contacts_adapter_profile adapter1=new Contacts_adapter_profile((ArrayList<String>) userProfile.getUserProfile().getContacts(),getActivity(),getContext());
            StaggeredGridLayoutManager llm = new StaggeredGridLayoutManager(1,StaggeredGridLayoutManager.HORIZONTAL);
            contacts.setLayoutManager(llm);
            contacts.setAdapter(adapter1);
            v.findViewById(R.id.links).setVisibility(View.VISIBLE);
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
            intent.putExtra("UserProfile",userProfile);
            getActivity().startActivity(intent);
        });

        return v;
    }
}
