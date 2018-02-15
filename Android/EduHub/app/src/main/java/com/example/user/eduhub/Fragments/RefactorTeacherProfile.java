package com.example.user.eduhub.Fragments;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
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

import com.example.user.eduhub.Adapters.SpinnerAdapterForSex;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.R;

import java.util.ArrayList;

import mabbas007.tagsedittext.TagsEditText;

/**
 * Created by User on 10.02.2018.
 */

public class RefactorTeacherProfile extends Fragment {
    ArrayList<String> contacts=new ArrayList<>();
    ArrayList<String> sexes=new ArrayList<>();
    String str;
    Boolean flag=false;
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.refactor_teacher_profile, null);
        ImageView addContact=v.findViewById(R.id.add_contacts);
        EditText editContact=v.findViewById(R.id.edit_contact);
        EditText editUserName=v.findViewById(R.id.name_user_profile2);
        EditText editUserEmail=v.findViewById(R.id.email_user_profile2);
        EditText editBirthYear=v.findViewById(R.id.Edit_birth_year);
        EditText editAboutMe=v.findViewById(R.id.EditAboutMe);
        TagsEditText editSkils=v.findViewById(R.id.edit_skils);
        Switch isTeacher=v.findViewById(R.id.isTeacher);
        Spinner sex=v.findViewById(R.id.sex);

        sexes.add("Мужской");
        sexes.add("Женский");
        SpinnerAdapterForSex adapter=new SpinnerAdapterForSex(getContext(),R.layout.spenner_item,sexes);
        sex.setAdapter(adapter);
        // заголовок
        sex.setPrompt("Пол");
        // выделяем элемент
        sex.setSelection(0);
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
        editContact.setOnEditorActionListener(new EditText.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == KeyEvent.KEYCODE_ENTER) {
                    contacts.add(editContact.getText().toString());
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
}
