package com.example.user.eduhub.Fragments;

import android.app.Activity;
import android.app.Fragment;
import android.os.Bundle;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Fakes.FakeReguest;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Fakes.TestUserRep;
import com.example.user.eduhub.Retrofit.AccountActivities;

/**
 * Created by user on 05.12.2017.
 */

public class LoginFragment extends Fragment {
IFragmentsActivities fragmentsActivities;
    TestUserRep testUserRep=new TestUserRep();
    AccountActivities accountActivities=new AccountActivities();
    FakeReguest fakeReguest=new FakeReguest();
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
                   checkUser=accountActivities.UserLogin(emailText.getText().toString(),password.getText().toString());
                   if(checkUser!=null){
                       MakeToast("Вход выполнен успешно "+checkUser.getEmail());
                       fragmentsActivities.signIn(checkUser);
                   }
                   else{MakeToast("Данной комбинации Email и пароля не существует");}
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



}
