package ru.lod_misis.user.eduhub.Fragments;

import android.app.Activity;

import android.content.Intent;
import android.graphics.Color;
import android.graphics.PorterDuff;
import android.os.Bundle;

import android.support.design.widget.TextInputLayout;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import ru.lod_misis.user.eduhub.AuthorizedUserActivity;
import ru.lod_misis.user.eduhub.Fakes.FakeLoginPresenter;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Interfaces.IFragmentsActivities;
import ru.lod_misis.user.eduhub.Interfaces.View.ILoginView;
import ru.lod_misis.user.eduhub.Models.LoginModel;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.LoginPresenter;
import com.example.user.eduhub.R;
import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;

import io.reactivex.disposables.Disposable;

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
    TextInputLayout errorLayoutEmail;
    TextInputLayout errorLayoutPassword;

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
        emailText.getBackground().mutate().setColorFilter(Color.WHITE, PorterDuff.Mode.SRC_ATOP);
        final EditText password = (EditText) v.findViewById(R.id.edit_password);
        password.getBackground().mutate().setColorFilter(Color.WHITE, PorterDuff.Mode.SRC_ATOP);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("");

        Button signIn = v.findViewById(R.id.sign_in);
        TextView signUp = v.findViewById(R.id.sign_up);
        signUp.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                RegistrationFragment registrationFragment = new RegistrationFragment();
                fragmentsActivities.switchingFragmets(registrationFragment);
            }
        });
        signIn.setOnClickListener(new View.OnClickListener() {
            public void onClick(View v) {
                if(!fakesButton.getCheckButton()){
                loginPresenter.Login(emailText.getText().toString(),password.getText().toString(),getContext());}
                else{
                    fakeLoginPresenter.Login(emailText.getText().toString(),password.getText().toString(),getContext());
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
                case 400:{MakeToast("Такого пользователя не существует");}
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
