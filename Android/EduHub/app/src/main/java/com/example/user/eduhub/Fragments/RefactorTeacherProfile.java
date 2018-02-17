package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.EditorInfo;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.Contacts_adapter;
import com.example.user.eduhub.Adapters.SpinnerAdapterForSex;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Interfaces.IRefreshList;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.R;

import java.util.ArrayList;

import mabbas007.tagsedittext.TagsEditText;

/**
 * Created by User on 10.02.2018.
 */

public class RefactorTeacherProfile extends Fragment implements IRefreshList{
    private UserProfileResponse userProfile;
    ArrayList<String> contacts=new ArrayList<>();
    ArrayList<String> sexes=new ArrayList<>();
    String str;
    String[] skils;
    Boolean flag=false;
    Contacts_adapter adapter1;
    IRefreshList refreshList;
    RecyclerView recyclerView;

    public void setUserProfile(UserProfileResponse userProfile) {
        this.userProfile = userProfile;
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.refactor_teacher_profile, null);
        refreshList=this;
        TextView userName=v.findViewById(R.id.name_user_profile);
        TextView userEmail=v.findViewById(R.id.email_user_profile);
        ImageView addContact=v.findViewById(R.id.add_contacts);
        EditText editContact=v.findViewById(R.id.edit_contact);
        EditText editUserName=v.findViewById(R.id.name_user_profile2);
        EditText editUserEmail=v.findViewById(R.id.email_user_profile2);
        EditText editBirthYear=v.findViewById(R.id.Edit_birth_year);
        EditText editAboutMe=v.findViewById(R.id.EditAboutMe);
        Button saveButton=v.findViewById(R.id.save_button);
        recyclerView=v.findViewById(R.id.contacts);
        TagsEditText editSkils=v.findViewById(R.id.edit_skils);
        Switch isTeacher=v.findViewById(R.id.isTeacher);
        Spinner sex=v.findViewById(R.id.sex);
        sexes.add("Мужской");
        sexes.add("Женский");
        SpinnerAdapterForSex adapter=new SpinnerAdapterForSex(getContext(),R.layout.spenner_item,sexes);
        sex.setAdapter(adapter);
        // заголовок
        sex.setPrompt("Пол");
        userName.setText(userProfile.getUserProfile().getName());
        userEmail.setText(userProfile.getUserProfile().getEmail());
        editUserName.setText(userProfile.getUserProfile().getName());
        editUserEmail.setText(userProfile.getUserProfile().getEmail());
        if(userProfile.getTeacherProfile().getSkills()!=null){
        skils=new String[userProfile.getTeacherProfile().getSkills().size()];
        for (int i=0;i<skils.length;i++){
            skils[i]=userProfile.getTeacherProfile().getSkills().get(i);
        }
        editSkils.setTags( skils);}
        if(userProfile.getUserProfile().getAboutUser()!=null){
        editAboutMe.setText(userProfile.getUserProfile().getAboutUser());}
        if(!userProfile.getUserProfile().getBirthYear().toString().equals("0")){
        editBirthYear.setText(userProfile.getUserProfile().getBirthYear()+"");}
        if(userProfile.getUserProfile().getContacts()!=null){
        contacts.addAll(userProfile.getUserProfile().getContacts());}
        recyclerView.setHasFixedSize(true);
        adapter1=new Contacts_adapter(contacts,getActivity(),getContext(),this);
        LinearLayoutManager llm = new LinearLayoutManager(getActivity().getApplicationContext());
        recyclerView.setLayoutManager(llm);
        recyclerView.setAdapter(adapter1);
        // устанавливаем обработчик нажатия
        sex.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                str= sexes.get(position);

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
        CardView cv=v.findViewById(R.id.addContactCard);
        addContact.setOnClickListener(click->{
            cv.setVisibility(View.GONE);
            editContact.setVisibility(View.VISIBLE);
        });
        editContact.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (keyCode == KeyEvent.KEYCODE_ENTER) {
                    contacts.add(editContact.getText().toString());
                    adapter1=new Contacts_adapter(contacts,getActivity(),getContext(),refreshList);
                    recyclerView.setAdapter(adapter1);
                    cv.setVisibility(View.VISIBLE);
                    editContact.setVisibility(View.GONE);
                    editContact.setText("");
                    return true;
                }
                return false;
            }
 });
        isTeacher.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                flag=b;
            }
        });
        return v;
    }

    @Override
    public void refreshContacts(ArrayList<String> contacts) {
        adapter1=new Contacts_adapter(contacts,getActivity(),getContext(),refreshList);
        recyclerView.setAdapter(adapter1);
    }
}
