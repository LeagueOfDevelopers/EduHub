package com.example.user.eduhub.Fragments;

import android.app.Activity;

import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.support.annotation.RequiresApi;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.text.InputType;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.Toast;


import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Fakes.FakeLoginPresenter;
import com.example.user.eduhub.Fakes.FakeRegistrPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Interfaces.View.ILoginView;
import com.example.user.eduhub.Interfaces.View.IRegistrView;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.LoginPresenter;
import com.example.user.eduhub.Presenters.RegistrPresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;
import com.suke.widget.SwitchButton;


import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

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
                if(name.getText().length()>0){
                    if(email.getText().length()>=6){
                        if(password.getText().length()>=6){
               isTeacherFlag=isTeacher.isChecked();
                if(!fakesButton.getCheckButton()){
                registrPresenter.RegistrationUser(name.getText().toString(),email.getText().toString(),password.getText().toString(),isTeacherFlag);}
                else{
                    fakeRegistrPresenter.RegistrationUser(name.getText().toString(),email.getText().toString(),password.getText().toString(),isTeacherFlag);
                }
                        }else{MakeToast("Пароль слишком короткий");}
                    }else{
                        MakeToast("Email слишком короткий");
                    }
                }else{MakeToast("Заполните поле имени");}

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
}
