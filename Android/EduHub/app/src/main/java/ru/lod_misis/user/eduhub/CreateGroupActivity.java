package ru.lod_misis.user.eduhub;

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
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Toast;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapter;
import ru.lod_misis.user.eduhub.Fakes.FakeCreateGroupPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.View.ICreateGroupView;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.CreateGroupPresenter;

import java.util.ArrayList;

import mabbas007.tagsedittext.TagsEditText;

public class CreateGroupActivity extends AppCompatActivity implements ICreateGroupView {
    ArrayList<String> tags=new ArrayList<>();
    String type;
    Spinner spinner;
    SpinnerAdapter adapter;
    Boolean privacy;
SharedPreferences sPref;
User user;
SavedDataRepository savedDataRepository=new SavedDataRepository();
FakesButton fakesButton=new FakesButton();
FakeCreateGroupPresenter fakeCreateGroupPresenter=new FakeCreateGroupPresenter(this);
    Boolean flag;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_group);
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        final EditText maxParticipants=findViewById(R.id.participants);
        final EditText cost=findViewById(R.id.cost);
        final TagsEditText tags=findViewById(R.id.tagsEditText);
        final CheckBox checkBox=findViewById(R.id.privacy);
        final EditText description=findViewById(R.id.about_group);
        ImageView plus=findViewById(R.id.plus);
        ImageView minus=findViewById(R.id.minus);
        Button createGroup=findViewById(R.id.create_group);
        user=savedDataRepository.loadSavedData(sPref);
        Toolbar toolbar=findViewById(R.id.toolbar);
        toolbar.setTitle("Создание группы");
        CreateGroupPresenter createGroupPresenter=new CreateGroupPresenter(this);
        ImageButton backButton=findViewById(R.id.back);
        CheckBox privacy=findViewById(R.id.privacy);
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
        String[] types={"Лекция","Мастер класс","Cеминар"};
        adapter=new SpinnerAdapter(this,R.layout.spenner_item, types);
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
                type=types[position];

            }
            @Override
            public void onNothingSelected(AdapterView<?> arg0) {
            }
        });
        EditText nameOfGroup=findViewById(R.id.name_of_group_create);

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
        createGroup.setOnClickListener(click->{
            if(!nameOfGroup.getText().toString().equals("")&&!cost.getText().toString().equals("")&&!maxParticipants.getText().toString().equals("")&&type!=null&&!tags.getText().toString().equals("")&&!description.getText()
            .toString().equals("")){
                if(Integer.valueOf(maxParticipants.getText().toString())<=200){
                    if(nameOfGroup.getText().length()>=3&&nameOfGroup.getText().length()<=70){
                        if(description.getText().length()>=20&&description.getText().length()<=3000){
                            if(tags.getTags().size()<=10&&tags.getTags().size()>=3) {
                                if (!fakesButton.getCheckButton()) {
                                    createGroupPresenter.createGroup(nameOfGroup.getText().toString(), description.getText().toString(), (ArrayList<String>) tags.getTags(), Integer.valueOf(maxParticipants.getText().toString()), Double.valueOf(cost.getText().toString()), type, flag, user.getToken(),this);
                                } else {
                                    fakeCreateGroupPresenter.createGroup(nameOfGroup.getText().toString(), description.getText().toString(), (ArrayList<String>) tags.getTags(), Integer.valueOf(maxParticipants.getText().toString()), Double.valueOf(cost.getText().toString()), type, flag, user.getToken(),this);
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
