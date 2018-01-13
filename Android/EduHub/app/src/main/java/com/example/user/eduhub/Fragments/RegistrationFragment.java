package com.example.user.eduhub.Fragments;

import android.app.Activity;

import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Models.Registration.RegistrationModel;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Fakes.TestUserRep;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by user on 06.12.2017.
 */

public class RegistrationFragment extends Fragment {
    IFragmentsActivities fragmentsActivities;
    TestUserRep testUserRep=new TestUserRep();

    String id;
    Disposable disposable;
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
        EditText inviteCode=v.findViewById(R.id.inviteCode);
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
                    RegistrationModel registrationModel=new RegistrationModel();
                    registrationModel.setEmail(email.getText().toString());
                    registrationModel.setName(login.getText().toString());
                    registrationModel.setPassword(password.getText().toString());
                    registrationModel.setIsTeacher(isTeacher);
                    registrationModel.setInviteCode(inviteCode.getText().toString());
                    registrationModel.setAvatarLink("String");
                    EduHubApi eduHubApi= RetrofitBuilder.getApi();
                     disposable=eduHubApi.userRegistration(registrationModel)
                            .subscribeOn(Schedulers.io())
                            .observeOn(AndroidSchedulers.mainThread())
                            .subscribe(
                                    next -> {
                                    },
                                    error -> {
                                        MakeToast("Ошибка");
                                    },
                                    ()->{MakeToast("Регистрация прошла успешно");
                                    LoginFragment fragment=new LoginFragment();
                                    fragmentsActivities.switchingFragmets(fragment);}



                            );

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
    public void onDestroy() {
        super.onDestroy();
        disposable.dispose();
    }


}
