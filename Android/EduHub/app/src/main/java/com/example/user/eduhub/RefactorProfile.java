package com.example.user.eduhub;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.CardView;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.Contacts_adapter;
import com.example.user.eduhub.Adapters.SpinnerAdapterForSex;
import com.example.user.eduhub.Fragments.RefactorTeacherProfile;
import com.example.user.eduhub.Fragments.RefactorUserProfile;
import com.example.user.eduhub.Interfaces.IRefreshList;
import com.example.user.eduhub.Interfaces.Presenters.IChangeUsersDataPresenter;
import com.example.user.eduhub.Interfaces.View.IChangeUsersDataView;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Models.UserProfile.UserProfileResponse;
import com.example.user.eduhub.Presenters.ChangeUserDataPresenter;

import java.util.ArrayList;

import mabbas007.tagsedittext.TagsEditText;

public class RefactorProfile extends AppCompatActivity implements IRefreshList,IChangeUsersDataView {
UserProfileResponse userProfile;
    ArrayList<String> contacts=new ArrayList<>();
    ArrayList<String> sexes=new ArrayList<>();
    String str;
    String[] skils;
    User user;
    Boolean flag=false;
    Contacts_adapter adapter1;
    IRefreshList refreshList;
    RecyclerView recyclerView;
    Activity activity=this;
    Context context=this;
    SharedPreferences sharedPreferences;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    ChangeUserDataPresenter changeUsersDataPresenter=new ChangeUserDataPresenter(this);
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_refactor_profile);
        ImageButton back=findViewById(R.id.back);
        Toolbar toolbar=findViewById(R.id.toolbar);
        toolbar.setTitle("Редактирование профиля");
        sharedPreferences=getSharedPreferences("User",MODE_PRIVATE);
        user=savedDataRepository.loadSavedData(sharedPreferences);
        Intent getIntent=getIntent();
        userProfile=(UserProfileResponse) getIntent.getSerializableExtra("UserProfile");


        refreshList=this;
        TextView userName=findViewById(R.id.name_user_profile);
        TextView userEmail=findViewById(R.id.email_user_profile);
        ImageView addContact=findViewById(R.id.add_contacts);
        EditText editContact=findViewById(R.id.edit_contact);
        EditText editUserName=findViewById(R.id.name_user_profile2);
        EditText editUserEmail=findViewById(R.id.email_user_profile2);
        EditText editBirthYear=findViewById(R.id.Edit_birth_year);
        EditText editAboutMe=findViewById(R.id.EditAboutMe);
        Button saveButton=findViewById(R.id.save_button);
        recyclerView=findViewById(R.id.contacts);
        TagsEditText editSkils=findViewById(R.id.edit_skils);
        Switch isTeacher=findViewById(R.id.isTeacher);
        isTeacher.setChecked(userProfile.getUserProfile().getIsTeacher());
        Spinner sex=findViewById(R.id.sex);
        sexes.add("Мужской");
        sexes.add("Женский");
        SpinnerAdapterForSex adapter=new SpinnerAdapterForSex(this,R.layout.spenner_item,sexes);
        sex.setAdapter(adapter);
        // заголовок
        sex.setPrompt("Пол");
        userName.setText(userProfile.getUserProfile().getName());
        userEmail.setText(userProfile.getUserProfile().getEmail());
        editUserName.setText(userProfile.getUserProfile().getName());
        editUserEmail.setText(userProfile.getUserProfile().getEmail());
        if(userProfile.getUserProfile().getIsTeacher()){
        if(userProfile.getTeacherProfile().getSkills()!=null){
            skils=new String[userProfile.getTeacherProfile().getSkills().size()];
            for (int i=0;i<skils.length;i++){
                skils[i]=userProfile.getTeacherProfile().getSkills().get(i);
            }
            editSkils.setTags( skils);}}else{
            findViewById(R.id.card_of_skils).setVisibility(View.GONE);
        }
        if(userProfile.getUserProfile().getAboutUser()!=null){
            editAboutMe.setText(userProfile.getUserProfile().getAboutUser());}
        if(!userProfile.getUserProfile().getBirthYear().toString().equals("0")){
            editBirthYear.setText(userProfile.getUserProfile().getBirthYear()+"");}
            else{editBirthYear.setText(userProfile.getUserProfile().getBirthYear()+"");}
        if(userProfile.getUserProfile().getContacts()!=null){
            contacts.addAll(userProfile.getUserProfile().getContacts());}
        recyclerView.setHasFixedSize(true);
        adapter1=new Contacts_adapter(contacts,this,this,this);
        LinearLayoutManager llm = new LinearLayoutManager(getApplicationContext());
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
        CardView cv=findViewById(R.id.addContactCard);
        addContact.setOnClickListener(click->{
            cv.setVisibility(View.GONE);
            editContact.setVisibility(View.VISIBLE);
        });
        editContact.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (keyCode == KeyEvent.KEYCODE_ENTER) {
                    contacts.add(editContact.getText().toString());
                    adapter1=new Contacts_adapter(contacts,activity,context,refreshList);
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
                userProfile.getUserProfile().setIsTeacher(b);
                if(b){
                    findViewById(R.id.card_of_skils).setVisibility(View.VISIBLE);
                }else{
                    findViewById(R.id.card_of_skils).setVisibility(View.GONE);
                }

            }
        });
        back.setOnClickListener(click->{
            onBackPressed();
        });
        saveButton.setOnClickListener(click->{
            changeUsersDataPresenter.changeUsersData(user.getToken(),editUserName.getText().toString(),editAboutMe.getText().toString(),contacts,Integer.valueOf(editBirthYear.getText().toString()),"string",str,userProfile.getUserProfile().getIsTeacher(),skils);
        });
    }

    @Override
    public void refreshContacts(ArrayList<String> contacts) {
        adapter1=new Contacts_adapter(contacts,this,this,refreshList);
        recyclerView.setAdapter(adapter1);
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
        onBackPressed();
    }
}
