package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.user.eduhub.Adapters.SpinnerAdapter;
import com.example.user.eduhub.Classes.TypeOfEducation;
import com.example.user.eduhub.Fakes.FakeCreateGroupPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.View.ICreateGroupView;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.CreateGroupPresenter;

import java.util.ArrayList;

public class CreateGroupActivity extends AppCompatActivity implements ICreateGroupView {
    ArrayList<String> tags=new ArrayList<>();
    TypeOfEducation type;
    Spinner spinner;
    SpinnerAdapter adapter;
    Boolean privacy;
SharedPreferences sPref;
User user;
SavedDataRepository savedDataRepository=new SavedDataRepository();
FakesButton fakesButton=new FakesButton();
FakeCreateGroupPresenter fakeCreateGroupPresenter=new FakeCreateGroupPresenter(this);


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_group);
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        user=savedDataRepository.loadSavedData(sPref);
        Toolbar toolbar=findViewById(R.id.toolbar);
        toolbar.setTitle("Создание группы");
        CreateGroupPresenter createGroupPresenter=new CreateGroupPresenter(this);
        ImageButton backButton=findViewById(R.id.back);
        final EditText nameOfGroup=findViewById(R.id.name);
        final EditText maxParticipants=findViewById(R.id.participants);
        final EditText cost=findViewById(R.id.cost);
        spinner=findViewById(R.id.type_of_education);
        final EditText editTags=findViewById(R.id.tags);
        final CheckBox checkBox=findViewById(R.id.checkBox);
        final EditText description=findViewById(R.id.Description);
        Button createGroup=findViewById(R.id.create_group);
        adapter=new SpinnerAdapter(this,R.layout.spenner_item, TypeOfEducation.values());

        spinner.setAdapter(adapter);
        // заголовок
        spinner.setPrompt("Type of education");
        // выделяем элемент
        spinner.setSelection(0);
        // устанавливаем обработчик нажатия
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                type=TypeOfEducation.values()[position];

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
        backButton.setOnClickListener(click->{
            Intent intent=new Intent(this,AuthorizedUserActivity.class);
            startActivity(intent);
        });
        createGroup.setOnClickListener(click->{
            if(!nameOfGroup.getText().toString().equals("")&&!cost.getText().toString().equals("")&&!maxParticipants.getText().toString().equals("")&&type!=null&&!editTags.getText().toString().equals("")&&!description.getText()
            .toString().equals("")){
                String[] str=editTags.getText().toString().split(" ");

                for(int i=0;i<str.length;i++){
                    tags.add(str[i]);
                }
                if (checkBox.isChecked()) {
                    privacy=true;
                }else{
                    privacy=false;
                }
                if(!fakesButton.getCheckButton()){
                createGroupPresenter.createGroup(nameOfGroup.getText().toString(),description.getText().toString(),tags,Integer.valueOf(maxParticipants.getText().toString()),Integer.valueOf( cost.getText().toString()),type,privacy,user.getToken());}
                else{
                    fakeCreateGroupPresenter.createGroup(nameOfGroup.getText().toString(),description.getText().toString(),tags,Integer.valueOf(maxParticipants.getText().toString()),Integer.valueOf( cost.getText().toString()),type,privacy,user.getToken());

                }
            }else{
                MakeToast("Заполните все поля");

            }
        });
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
        MakeToast("Группа успешно создана");
        Intent intent=new Intent(this,AuthorizedUserActivity.class);
        startActivity(intent);
    }
    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }
}
