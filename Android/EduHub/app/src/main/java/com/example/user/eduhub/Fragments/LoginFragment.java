package com.example.user.eduhub.Fragments;

import android.app.Activity;

import android.content.Intent;
import android.os.Bundle;

import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import com.example.user.eduhub.AuthorizedUserActivity;
import com.example.user.eduhub.Fakes.FakeLoginPresenter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Interfaces.View.ILoginView;
import com.example.user.eduhub.Models.LoginModel;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.LoginPresenter;
import com.example.user.eduhub.R;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;
import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

/**
 * Created by user on 05.12.2017.
 */

public class LoginFragment extends Fragment implements ILoginView {

    IFragmentsActivities fragmentsActivities;
    LoginPresenter loginPresenter=new LoginPresenter(this);
    LoginModel loginModel;
    User user;
    Disposable disposable;
    FakesButton fakesButton=new FakesButton();
    FakeLoginPresenter fakeLoginPresenter=new FakeLoginPresenter(this);

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
        final EditText emailText = (EditText) v.findViewById(R.id.edit_email);
        final EditText password = (EditText) v.findViewById(R.id.edit_password);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("Вход");
        Button signIn = v.findViewById(R.id.sign_in);
        Button signUp = v.findViewById(R.id.sign_up);
        signUp.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                RegistrationFragment registrationFragment = new RegistrationFragment();
                fragmentsActivities.switchingFragmets(registrationFragment);
            }
        });
        signIn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(!fakesButton.getCheckButton()){
                loginPresenter.Login(emailText.getText().toString(),password.getText().toString());}
                else{
                    fakeLoginPresenter.Login(emailText.getText().toString(),password.getText().toString());
                }





            }
        });

        return v;

    }

    private void MakeToast(String s) {
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
    public void getError(Throwable error) {
        if(error instanceof HttpException){
            switch (((HttpException) error).code()){
                case 401:{MakeToast("Такого сочетания email и пароля не найдено");}
            }
        }
    }

    @Override
    public void login(User user) {
        Intent intent = new Intent(getActivity(), AuthorizedUserActivity.class);
        intent.putExtra("user", user);
        getActivity().startActivity(intent);
    }
}
