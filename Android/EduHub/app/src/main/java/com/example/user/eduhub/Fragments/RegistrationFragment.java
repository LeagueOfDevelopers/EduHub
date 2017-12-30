package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.ICallBack;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Fakes.TestUserRep;
import com.example.user.eduhub.Retrofit.AccountActivities;

/**
 * Created by user on 06.12.2017.
 */

public class RegistrationFragment extends Fragment implements ICallBack {
    IFragmentsActivities fragmentsActivities;
    TestUserRep testUserRep=new TestUserRep();
    AccountActivities accountActivities=new AccountActivities(this);
    String id;
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
        final EditText email=v.findViewById(R.id.registr_email);
        final EditText password=v.findViewById(R.id.registr_password);
        final EditText login=v.findViewById(R.id.registr_login);
        final CheckBox checkBox=v.findViewById(R.id.teacher_or_not);
        Button submit=v.findViewById(R.id.registr_btn);


        submit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                if(!email.getText().toString().equals("")&&!password.getText().toString().equals("")&&!login.getText().toString().equals("")){
                    boolean isTeacher;
                    if(checkBox.isChecked()){
                        isTeacher=true;
                    } else{
                        isTeacher=false;
                    }
                    accountActivities.UserRegistration(email.getText().toString(),password.getText().toString(),login.getText().toString(),isTeacher);


                }else{
                   MakeToast("Ошибка.заполните все поля.");

                }

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
    public void callBackRegistrate(String id) {
        this.id=id;
        MakeToast("Регистрация прошла успешно");
    }

    @Override
    public void callBackRegistrationError(int code) {

    }

    @Override
    public void callBackLogin(String token) {

    }

    @Override
    public void callBackLoginError(int code) {

    }
}
