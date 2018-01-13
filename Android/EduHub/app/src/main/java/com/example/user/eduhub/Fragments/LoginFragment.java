package com.example.user.eduhub.Fragments;

import android.app.Activity;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;

import android.support.v4.app.Fragment;
import android.support.v4.content.SharedPreferencesCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Fakes.TestUserRep;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.prefs.Preferences;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by user on 05.12.2017.
 */

public class LoginFragment extends Fragment {

IFragmentsActivities fragmentsActivities;
    TestUserRep testUserRep=new TestUserRep();
    LoginModel loginModel;
    User user;
    Disposable disposable;

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
                  loginModel=new LoginModel();

                  loginModel.setEmail(emailText.getText().toString());
                  loginModel.setPassword(password.getText().toString());
                    EduHubApi eduHubApi= RetrofitBuilder.getApi();
                    disposable= eduHubApi.userLogin(loginModel)
                            .subscribeOn(Schedulers.io())
                            .observeOn(AndroidSchedulers.mainThread())
                            .subscribe(response->{
                                user=response;
                                MakeToast("Вход выполнен успешно");
                                Intent intent=new Intent(getActivity(),AuthorizedUserActivity.class);
                                intent.putExtra("user",user);
                                getActivity().startActivity(intent);},
                                    error->{});
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
    public void onDestroy() {
        super.onDestroy();
        disposable.dispose();
    }
}
