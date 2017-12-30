package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Interfaces.ICallBack;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Fakes.TestUserRep;
import com.example.user.eduhub.Retrofit.AccountActivities;

/**
 * Created by user on 05.12.2017.
 */

public class LoginFragment extends Fragment implements ICallBack {
IFragmentsActivities fragmentsActivities;
    TestUserRep testUserRep=new TestUserRep();
    AccountActivities accountActivities=new AccountActivities(this);
    boolean flag=false;
    User checkUser;
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }
    public View onCreateView(LayoutInflater inflater, final ViewGroup container,
                             Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.login_fragment, null);
        final EditText emailText=(EditText)v.findViewById(R.id.edit_email);
        final EditText password=(EditText)v.findViewById(R.id.edit_password);
        Button signIn= v.findViewById(R.id.sign_in);
        Button signUp= v.findViewById(R.id.sign_up);
        signUp.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                RegistrationFragment registrationFragment=new RegistrationFragment();
                fragmentsActivities.switchingFragmets(registrationFragment);

            }
        });
        signIn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                flag=false;
                if(!emailText.getText().toString().equals("")&&!password.getText().toString().equals("")){
                   accountActivities.UserLogin(emailText.getText().toString(),password.getText().toString());

                }
                else{
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

    }

    @Override
    public void callBackRegistrationError(int code) {

    }

    @Override
    public void callBackLogin(String token) {
        MakeToast("Вход выполнен успешно");
        Intent intent=new Intent(getActivity(), AuthorizedUserActivity.class);
        intent.putExtra("token",token);
        startActivity(intent);

    }

    @Override
    public void callBackLoginError(int code) {
        if(code==401){
            MakeToast("Неверный логин или пароль");
        }
    }
}
