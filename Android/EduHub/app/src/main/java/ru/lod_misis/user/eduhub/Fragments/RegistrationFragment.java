package ru.lod_misis.user.eduhub.Fragments;

import android.app.Activity;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;


import ru.lod_misis.user.eduhub.AuthorizedUserActivity;
import ru.lod_misis.user.eduhub.Fakes.FakeLoginPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakeRegistrPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IFragmentsActivities;
import ru.lod_misis.user.eduhub.Interfaces.View.ILoginView;
import ru.lod_misis.user.eduhub.Interfaces.View.IRegistrView;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.LoginPresenter;
import ru.lod_misis.user.eduhub.Presenters.RegistrPresenter;
import com.example.user.eduhub.R;
import com.suke.widget.SwitchButton;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by user on 06.12.2017.
 */

public class RegistrationFragment extends Fragment implements IRegistrView,ILoginView {
    IFragmentsActivities fragmentsActivities;
    RegistrPresenter registrPresenter=new RegistrPresenter(this);
    LoginPresenter loginPresenter=new LoginPresenter(this);
    String id;
    EditText name;
    EditText password;
    EditText email;
    CheckBox checkBox2;
    FakesButton fakesButton=new FakesButton();
    FakeRegistrPresenter fakeRegistrPresenter=new FakeRegistrPresenter(this);
    FakeLoginPresenter fakeLoginPresenter=new FakeLoginPresenter(this);
    SwitchButton isTeacher;


    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }

    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.registration_fragment, null);
        email=v.findViewById(R.id.registr_email);
        password=v.findViewById(R.id.registr_password);
        int closePassword=password.getInputType();
        isTeacher=v.findViewById(R.id.teacher_or_not);
        name=v.findViewById(R.id.registr_login);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Регистрация");


        Button submit=v.findViewById(R.id.registr_btn);

        submit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                boolean isTeacherFlag;
                if(name.getText().length()>=3&&name.getText().length()<=70){
                    if(checkName(name.getText().toString())){
                    if(email.getText().length()>0){
                        if(password.getText().length()>=8&&password.getText().length()<=50){
               isTeacherFlag=isTeacher.isChecked();
                if(!fakesButton.getCheckButton()){
                registrPresenter.RegistrationUser(name.getText().toString(),email.getText().toString(),password.getText().toString(),isTeacherFlag);}
                else{
                    fakeRegistrPresenter.RegistrationUser(name.getText().toString(),email.getText().toString(),password.getText().toString(),isTeacherFlag);
                }
                        }else{MakeToast("Минимальная длина пароля - 8 символов,максимальная - 50");}
                    }else{
                        MakeToast("Заполните поле Email");
                    }}else{
                        MakeToast("Допустимые символы для имени-[a-z,A-Z,а-я,А-Я]");
                    }
                }else{MakeToast("Минимальная длина имени - 3 символа,максимальная - 70");}

            }
        });
        return v;
    }
    private void MakeToast(String s){
        Toast toast = Toast.makeText(getActivity().getApplicationContext(),
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
    public void getResponse(Fragment fragment) {
        MakeToast("Регистрация выполнена успешно.");
        if(!fakesButton.getCheckButton()){
        loginPresenter.Login(email.getText().toString(),password.getText().toString());}
        else{
            fakeLoginPresenter.Login(email.getText().toString(),password.getText().toString());
        }

    }

    @Override
    public void getError(Throwable error) {

    }

    @Override
    public void login(User user) {
        Intent intent = new Intent(getActivity(), AuthorizedUserActivity.class);
        intent.putExtra("user", user);
        getActivity().startActivity(intent);
    }
    private boolean checkName(String name){
        Pattern p=Pattern.compile("^[a-z,A-Z,А-Я,а-я]+");
        Matcher m=p.matcher(name);
        return m.matches();
    }
}
