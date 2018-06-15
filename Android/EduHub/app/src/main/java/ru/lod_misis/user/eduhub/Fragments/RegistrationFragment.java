package ru.lod_misis.user.eduhub.Fragments;

import android.app.Activity;

import android.content.Intent;
import android.graphics.Color;
import android.graphics.PorterDuff;
import android.os.Bundle;
import android.support.design.widget.TextInputEditText;
import android.support.design.widget.TextInputLayout;
import android.support.v4.app.Fragment;
import android.support.v7.widget.Toolbar;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.EditText;
import android.widget.Switch;
import android.widget.TextView;
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
    TextInputLayout errorLayoutName;
    TextInputLayout errorLayoutEmail;
    TextInputLayout errorLayoutPassword;
    FakesButton fakesButton=new FakesButton();
    FakeRegistrPresenter fakeRegistrPresenter=new FakeRegistrPresenter(this);
    FakeLoginPresenter fakeLoginPresenter=new FakeLoginPresenter(this);
    Switch isTeacher;


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
        email.getBackground().mutate().setColorFilter(Color.WHITE, PorterDuff.Mode.SRC_ATOP);
        password=v.findViewById(R.id.registr_password);
        password.getBackground().mutate().setColorFilter(Color.WHITE, PorterDuff.Mode.SRC_ATOP);
        isTeacher=v.findViewById(R.id.teacher_or_not);
        name=v.findViewById(R.id.registr_login);
        name.getBackground().mutate().setColorFilter(Color.WHITE, PorterDuff.Mode.SRC_ATOP);
        Toolbar toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle("");
        errorLayoutEmail=v.findViewById(R.id.error_layout_registr_email);
        errorLayoutName=v.findViewById(R.id.error_layout_registr_login);
        errorLayoutPassword=v.findViewById(R.id.registr_password2);
        TextView back=v.findViewById(R.id.sign_in);
back.setOnClickListener(click->{
    LoginFragment loginFragment = new LoginFragment();
    fragmentsActivities.switchingFragmets(loginFragment);
});
        Button submit=v.findViewById(R.id.registr_btn);

        submit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                boolean isTeacherFlag;
                if(name.getText().length()>=3&&name.getText().length()<=70){
                    errorLayoutName.setErrorEnabled(false);
                    if(checkName(name.getText().toString())){
                        errorLayoutName.setErrorEnabled(false);
                    if(email.getText().length()>0){
                        errorLayoutEmail.setErrorEnabled(false);
                        if(password.getText().length()>=8&&password.getText().length()<=50){
                            errorLayoutPassword.setErrorEnabled(false);
               isTeacherFlag=false;
                if(!fakesButton.getCheckButton()){
                registrPresenter.RegistrationUser(name.getText().toString(),email.getText().toString(),password.getText().toString(),isTeacherFlag,getContext());}
                else{
                    fakeRegistrPresenter.RegistrationUser(name.getText().toString(),email.getText().toString(),password.getText().toString(),isTeacherFlag,getContext());
                }
                        }else{errorLayoutPassword.setErrorEnabled(true);
                        errorLayoutPassword.setError("Минимальная длина пароля - 8 символов,максимальная - 50");
                            MakeToast("Минимальная длина пароля - 8 символов,максимальная - 50");}
                    }else{
                        errorLayoutEmail.setErrorEnabled(true);
                        errorLayoutEmail.setError("Заполните поле Email");
                        MakeToast("Заполните поле Email");
                    }}else{
                        errorLayoutName.setErrorEnabled(true);
                        errorLayoutName.setError("Допустимые символы для имени-[a-z,A-Z,а-я,А-Я]");
                        MakeToast("Допустимые символы для имени-[a-z,A-Z,а-я,А-Я]");
                    }

                }else{
                    errorLayoutName.setErrorEnabled(true);
                    errorLayoutName.setError("Минимальная длина имени - 3 символа,максимальная - 70");
                    MakeToast("Минимальная длина имени - 3 символа,максимальная - 70");}

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
        loginPresenter.Login(email.getText().toString(),password.getText().toString(),getContext());}
        else{
            fakeLoginPresenter.Login(email.getText().toString(),password.getText().toString(),getContext());
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
