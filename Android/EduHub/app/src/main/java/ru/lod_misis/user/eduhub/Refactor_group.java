package ru.lod_misis.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.IChangeGroupSettingsView;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.ChangeGroupSettingsPresenter;

import java.util.ArrayList;

import mabbas007.tagsedittext.TagsEditText;

public class Refactor_group extends AppCompatActivity implements IChangeGroupSettingsView {

    ArrayList<String> tags=new ArrayList<>();
    String type;
    Spinner spinner;
    SpinnerAdapter adapter;
    Boolean privacy;
    SharedPreferences sPref;
    User user;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    FakesButton fakesButton=new FakesButton();
    Boolean flag;
    Group group;
    ChangeGroupSettingsPresenter changeGroupSettingsPresenter=new ChangeGroupSettingsPresenter(this);
    String[] tagsArray;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_refactor_group);
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        final EditText maxParticipants=findViewById(R.id.participants);
        final EditText cost=findViewById(R.id.cost);
        final TagsEditText tags=findViewById(R.id.tagsEditText);
        final EditText description=findViewById(R.id.about_group);
        ImageView plus=findViewById(R.id.plus);
        ImageView minus=findViewById(R.id.minus);
        Button save_changes=findViewById(R.id.change_settings);
        user=(User) savedDataRepository.loadSavedData(sPref);
        Log.d("UserToken",user.getToken());
        Toolbar toolbar=findViewById(R.id.toolbar);
        toolbar.setTitle("Редактирование группы");
        Intent intent=getIntent();
        group=(Group)getIntent().getSerializableExtra("Group");
        ImageButton backButton=findViewById(R.id.back);
        CheckBox privacy=findViewById(R.id.privacy);
        cost.setText(group.getGroupInfo().getCost().toString());
        description.setText(group.getGroupInfo().getDescription());
        flag=false;
        privacy.setOnClickListener(click->{
            if(flag){
                privacy.setButtonDrawable(R.drawable.ic_black_circle);
                flag=false;
            }else{
                privacy.setButtonDrawable(R.drawable.ic_check_circle_black_24dp);
                flag=true;
            }
        });
        spinner=findViewById(R.id.type_of_education);
        String[] types={"Лекция","Мастер класс","Семинар"};
        adapter=new SpinnerAdapter(this,R.layout.spenner_item, types);
        spinner.setAdapter(adapter);

        // заголовок
        spinner.setPrompt("Type of education");

        // устанавливаем обработчик нажатия
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view,
                                       int position, long id) {
                // показываем позиция нажатого элемента
                type=types[position];

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
        tagsArray=new String[group.getGroupInfo().getTags().size()];
        for(int i=0;i<group.getGroupInfo().getTags().size();i++){
            tagsArray[i]=group.getGroupInfo().getTags().get(i);
        }
        tags.setTags(tagsArray);
        EditText nameOfGroup=findViewById(R.id.name_of_group_create);
        nameOfGroup.setText(group.getGroupInfo().getTitle());
        maxParticipants.setText(group.getGroupInfo().getSize().toString());
        plus.setOnClickListener(click->{
            if(maxParticipants.getText().toString().equals("")){
                maxParticipants.setText("0");
            }
            maxParticipants.setText((Integer.valueOf(maxParticipants.getText().toString())+1)+"");
        });

        minus.setOnClickListener(click->{
            if(maxParticipants.getText().toString().equals("")){
                maxParticipants.setText("0");
            }if(Integer.valueOf(maxParticipants.getText().toString())>=0){
                maxParticipants.setText((Integer.valueOf(maxParticipants.getText().toString())-1)+"");
            }
        });



        backButton.setOnClickListener(click->{
            onBackPressed();
        });






        save_changes.setOnClickListener(click->{
            if(!nameOfGroup.getText().toString().equals("")&&!cost.getText().toString().equals("")&&!maxParticipants.getText().toString().equals("")&&type!=null&&!tags.getText().toString().equals("")&&!description.getText()
                    .toString().equals("")){if(Integer.valueOf(maxParticipants.getText().toString())<=200){
                if(nameOfGroup.getText().length()>=3&&nameOfGroup.getText().length()<=70){
                    if(description.getText().length()>=20&&description.getText().length()<=3000){
                        if(tags.getTags().size()<=10&&tags.getTags().size()>=3) {
                    if(!fakesButton.getCheckButton()){
                        changeGroupSettingsPresenter.chngeGroupSettings(user.getToken(),group.getGroupInfo().getId(),nameOfGroup.getText().toString(),Integer.valueOf(maxParticipants.getText().toString()),Double.valueOf(cost.getText().toString()),type,(ArrayList<String>)tags.getTags(),description.getText().toString(),privacy.isChecked(),this);
                        }
                    else{
                        onBackPressed();
                    }
                }else{
                    MakeToast("Минимальное кол-во тэгов - 3,максимальное - 10");
                }
            } else{
                MakeToast("Минимальная длина описания группы - 20 символов,максимальная - 3000");
            }
        } else {
            MakeToast("Минимальная длина названия группы - 3 символа,максимальная - 70");
        }
    } else{MakeToast("Максимальный размер группы 200");}
}else {
        MakeToast("Заполните все поля");

        }
        });
    }


    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
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

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        Intent intent=new Intent(this,GroupActivity.class);
        intent.putExtra("group",group);
        startActivity(intent);
    }
}
