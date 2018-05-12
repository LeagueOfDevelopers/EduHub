package ru.lod_misis.user.eduhub;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.support.design.widget.TextInputLayout;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.CardView;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.KeyEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.Switch;
import android.widget.TextView;
import android.widget.Toast;

import ru.lod_misis.user.eduhub.Adapters.Contacts_adapter;
import ru.lod_misis.user.eduhub.Adapters.SpinnerAdapterForSex;
import ru.lod_misis.user.eduhub.Interfaces.IRefreshList;
import ru.lod_misis.user.eduhub.Interfaces.View.IChangeUsersDataView;
import ru.lod_misis.user.eduhub.Interfaces.View.IFileRepositoryView;
import ru.lod_misis.user.eduhub.Interfaces.View.IFindTagsView;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.DecodeFile;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Models.UserProfile.UserProfileResponse;
import ru.lod_misis.user.eduhub.Presenters.ChangeUserDataPresenter;
import ru.lod_misis.user.eduhub.Presenters.FileRepository;

import com.example.user.eduhub.R;
import com.squareup.picasso.Picasso;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;
import java.util.TimeZone;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import mabbas007.tagsedittext.TagsEditText;
import okhttp3.ResponseBody;
import ru.lod_misis.user.eduhub.Presenters.FindTagsPresenter;

public class RefactorProfile extends AppCompatActivity implements IRefreshList,IChangeUsersDataView,IFileRepositoryView,IFindTagsView {
UserProfileResponse userProfile;
    ArrayList<String> contacts=new ArrayList<>();
    ArrayList<String> sexes=new ArrayList<>();
    String str;
    String[] skils;
    User user;
    Uri uri=null;
    Boolean flag=false;
    ImageView avatar;
    TextView status;
    Contacts_adapter adapter1;
    IRefreshList refreshList;
    RecyclerView recyclerView;
    Activity activity=this;
    Context context=this;
    String avatarLink;
    ImageView addContact;
    TextView headerEditContact;
    TextView headerSkils;
    EditText editUserName;
    EditText editContact;
    EditText editUserEmail;
    EditText editBirthYear;
    EditText editAboutMe;
    TextInputLayout textInputLayoutName;
    TextInputLayout textInputLayoutEmail;
    TextInputLayout textInputLayoutAboutMe;
    TextInputLayout textInputLayoutBirthYear;
    TextInputLayout textInputLayoutSkills;
    TextInputLayout textInputLayoutContacts;
    TagsEditText editSkils;
    SharedPreferences sharedPreferences;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    ChangeUserDataPresenter changeUsersDataPresenter=new ChangeUserDataPresenter(this);
    FindTagsPresenter findTagsPresenter;
    FileRepository fileRepository;
    ImageButton edittContactBtn;
    DecodeFile decodeFile=new DecodeFile(this);
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
        fileRepository=new FileRepository(this,this);
        headerEditContact=findViewById(R.id.header_edit_contact);
        findTagsPresenter=new FindTagsPresenter(this,this);
        headerSkils=findViewById(R.id.header_skils);
        edittContactBtn=findViewById(R.id.edit_contact_btn);

        refreshList=this;
        status=findViewById(R.id.status);
        TextView userName=findViewById(R.id.name_user_profile);

        avatar=findViewById(R.id.avatar);
        if(userProfile.getUserProfile().getAvatarLink()!=null){
            avatarLink=userProfile.getUserProfile().getAvatarLink();
            Picasso.get().load("http://85.143.104.47:2411/api/file/img/"+userProfile.getUserProfile().getAvatarLink()).into(avatar);
        }


         addContact=findViewById(R.id.add_contacts);
         editContact=findViewById(R.id.edit_contact);
         editUserName=findViewById(R.id.name_user_profile2);
         editUserEmail=findViewById(R.id.email_user_profile2);
         editBirthYear=findViewById(R.id.birth_year);
         editAboutMe=findViewById(R.id.EditAboutMe);
        Button saveButton=findViewById(R.id.save_button);
        recyclerView=findViewById(R.id.contacts);
         editSkils=findViewById(R.id.edit_skils);

        Switch isTeacher=findViewById(R.id.isTeacher);
        isTeacher.setChecked(userProfile.getUserProfile().getIsTeacher());
        Spinner sex=findViewById(R.id.sex);
        sexes.add("Неизвестно");
        sexes.add("Мужской");
        sexes.add("Женский");
        textInputLayoutAboutMe=findViewById(R.id.error_layout_aboutMe);
        textInputLayoutBirthYear=findViewById(R.id.error_layout_birth_year);
        textInputLayoutEmail=findViewById(R.id.error_layout_email);
        textInputLayoutName=findViewById(R.id.error_layout_name);
        textInputLayoutSkills=findViewById(R.id.error_layout_skills);
        textInputLayoutContacts=findViewById(R.id.error_layout_contact);

        SpinnerAdapterForSex adapter=new SpinnerAdapterForSex(this,R.layout.spenner_item,sexes);
        sex.setAdapter(adapter);
        // заголовок
        sex.setPrompt("Пол");
        userName.setText(userProfile.getUserProfile().getName());

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
            else{editBirthYear.setText("");}

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
        avatar.setOnClickListener(click->{
            Intent intent=new Intent(Intent.ACTION_PICK);
            intent.setType("image/*");
            if (intent.resolveActivity(getPackageManager()) != null) {
                startActivityForResult(intent,1);
            }
        });
        editSkils.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {

            }

            @Override
            public void afterTextChanged(Editable s) {
                if(!s.equals("")){
                   String[] str= s.toString().split(" ");
                    findTagsPresenter.findTags(str[str.length-1]);
                    Log.d("tag",s.toString());
                }
            }
        });
        addContact.setOnClickListener(click->{
            if(contacts.size()<=5){
            textInputLayoutContacts.setVisibility(View.VISIBLE);
            edittContactBtn.setVisibility(View.VISIBLE);
            addContact.setVisibility(View.GONE);
            headerEditContact.setVisibility(View.GONE);
            editContact.setVisibility(View.VISIBLE);}else{
                MakeToast("Максимальное кол-во контактов-5");
            }
        });
        editContact.setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (keyCode == KeyEvent.KEYCODE_ENTER) {

                    if(checkLink(editContact.getText().toString())){
                        textInputLayoutContacts.setErrorEnabled(false);
                    contacts.add(editContact.getText().toString());
                    adapter1=new Contacts_adapter(contacts,activity,context,refreshList);
                    recyclerView.setAdapter(adapter1);
                    headerEditContact.setVisibility(View.VISIBLE);
                    addContact.setVisibility(View.VISIBLE);
                    editContact.setVisibility(View.GONE);
                    editContact.setText("");
                        textInputLayoutContacts.setVisibility(View.GONE);
                        edittContactBtn.setVisibility(View.GONE);
                    return true;}else{
                        textInputLayoutContacts.setErrorEnabled(true);
                        textInputLayoutContacts.setError("Введена некорректная ссылка");
                        MakeToast("Введена некорректная ссылка");
                    }
                }
                return false;
            }
        });
        edittContactBtn.setOnClickListener(click->{
            if(checkLink(editContact.getText().toString())){
                textInputLayoutContacts.setErrorEnabled(false);
                contacts.add(editContact.getText().toString());
                adapter1=new Contacts_adapter(contacts,activity,context,refreshList);
                recyclerView.setAdapter(adapter1);
                headerEditContact.setVisibility(View.VISIBLE);
                addContact.setVisibility(View.VISIBLE);
                editContact.setVisibility(View.GONE);
                editContact.setText("");
                textInputLayoutContacts.setVisibility(View.GONE);
                edittContactBtn.setVisibility(View.GONE);
                }else{
                textInputLayoutContacts.setErrorEnabled(true);
                textInputLayoutContacts.setError("Введена некорректная ссылка");
                MakeToast("Введена некорректная ссылка");
            }
        });
        isTeacher.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton compoundButton, boolean b) {
                userProfile.getUserProfile().setIsTeacher(b);
                if(b){
                    headerSkils.setVisibility(View.VISIBLE);
                    editSkils.setVisibility(View.VISIBLE);
                }else{
                    headerSkils.setVisibility(View.GONE);
                    editSkils.setVisibility(View.GONE);
                }

            }
        });
        back.setOnClickListener(click->{
            onBackPressed();
        });
        saveButton.setOnClickListener(click->{

            if(editUserName.getText().length()>=3&&editAboutMe.getText().length()<=70) {
                textInputLayoutName.setErrorEnabled(false);
                if(checkName(editUserName.getText().toString())){
                    textInputLayoutName.setErrorEnabled(false);
                    if((editAboutMe.getText().length()<=3000&&editAboutMe.getText().length()>=20)||editAboutMe.getText().toString().equals("")){
                        textInputLayoutAboutMe.setErrorEnabled(false);
                        if(editBirthYear.getText().toString().equals("")||(Integer.valueOf(editBirthYear.getText().toString())>=1900&&
                                Integer.valueOf(editBirthYear.getText().toString())<= getCurrentYear())){
                            textInputLayoutBirthYear.setErrorEnabled(false);
                            skils=new String[editSkils.getTags().size()];
                            for (int i=0;i<editSkils.getTags().size();i++){
                                skils[i]=editSkils.getTags().get(i);
                            }
                                if(uri!=null){
                fileRepository.loadImageToServer(user.getToken(),uri);
                }else {

            if(editBirthYear.getText().toString().equals("")){
                editBirthYear.setText("0");
            }
            Log.d("12344454",editSkils.getTags().size()+"");

            changeUsersDataPresenter.changeUsersData(user.getToken(),editUserName.getText().toString(),editAboutMe.getText().toString(),contacts,Integer.valueOf(editBirthYear.getText().toString()),avatarLink,str,userProfile.getUserProfile().getIsTeacher(),skils,this);

                                }

                        }else{
                            textInputLayoutBirthYear.setErrorEnabled(true);
                            textInputLayoutBirthYear.setError("Минимальынй допустимый год рождения - 1900,максимальный - "+getCurrentYear());
                            MakeToast("Минимальынй допустимый год рождения - 1900,максимальный - "+getCurrentYear());
                        }
                    }else{
                        textInputLayoutAboutMe.setErrorEnabled(true);
                        textInputLayoutAboutMe.setError("Пожалуйста,напишите больше информации о себе");
                        MakeToast("Пожалуйста,напишите больше информации о себе");
                    }
                }else{
                    textInputLayoutName.setErrorEnabled(true);
                    textInputLayoutName.setError("Допустимые символы для имени-[a-z,A-Z,а-я,А-Я]");
                    MakeToast("Допустимые символы для имени-[a-z,A-Z,а-я,А-Я]");
                }
            }else{
                textInputLayoutName.setErrorEnabled(true);
                textInputLayoutName.setError("Минимальная длина имени - 3 символа,максимальная - 70");
                MakeToast("Минимальная длина имени - 3 символа,максимальная - 70");
            }


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
        savedDataRepository.SaveUser(user.getToken(),editUserName.getText().toString(),avatarLink,user.getEmail(),
                userProfile.getUserProfile().getIsTeacher(),sharedPreferences);
        onBackPressed();
    }

    @Override
    public void getResponse(AddFileResponseModel addFileResponseModel) {
        avatarLink=addFileResponseModel.getFileName();
        Log.d("FilePathForGetImage",addFileResponseModel.getFileName());
        Picasso.get().load(addFileResponseModel.getFileName()).into(avatar);
        if(editBirthYear.getText().toString().equals("")){
            editBirthYear.setText("0");
        }
        changeUsersDataPresenter.changeUsersData(user.getToken(),editUserName.getText().toString(),editAboutMe.getText().toString(),contacts,Integer.valueOf(editBirthYear.getText().toString()),avatarLink,str,userProfile.getUserProfile().getIsTeacher(),skils,this);
        user.setAvatarLink(avatarLink);
        SharedPreferences.Editor editor=sharedPreferences.edit();
        editor.clear();
        editor.commit();
        savedDataRepository.SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail(),user.getTeacher(),sharedPreferences);


    }

    @Override
    public void getFile(ResponseBody file) {
        Log.d("FIleTest",file.toString());
        byte[] rawBitmap;
        try {rawBitmap=file.bytes();
            Log.d("Проверяемчтозахрень тут",rawBitmap[5]+"");
            Bitmap bitmap = BitmapFactory.decodeByteArray(rawBitmap,0,rawBitmap.length);


            avatar.setImageBitmap(bitmap);
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if(resultCode==RESULT_OK)
            if(requestCode==1){
                uri=data.getData();

                Picasso.get().load(uri).into(avatar);
                Log.d("Path",uri.toString());


            }
    }
    private boolean checkName(String name){
        Pattern p=Pattern.compile("^[a-z,A-Z,А-Я,а-я]+");
        Matcher m=p.matcher(name);
        return m.matches();
    }
    private void MakeToast(String s) {
        Toast toast = Toast.makeText(getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }
    private int getCurrentYear(){
        Calendar calendar=Calendar.getInstance(TimeZone.getDefault(), Locale.getDefault());
        calendar.setTime(new Date());
        return calendar.get(Calendar.YEAR);
    }
    private Boolean checkLink(String link){
        Log.d("Link",link);
        Pattern p=Pattern.compile("^vk.com/\\S+$|^@\\S+$|^twitter.com/\\S+$|^ok.ru\\S+$|^facebook.com/\\S+$" +
                "|^instagram.com/\\S+$");
        Matcher m=p.matcher(link);
        return m.matches();
    }

    @Override
    public void getTags(ArrayList<String> tags) {
        Log.d("tag",tags.size()+"");
        editSkils.setAdapter(new ArrayAdapter<>(this,
                android.R.layout.simple_dropdown_item_1line,tags ));

    }
}
