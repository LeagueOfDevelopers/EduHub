package com.example.user.eduhub;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.example.user.eduhub.Classes.User;
import com.example.user.eduhub.Fragments.LoginFragment;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;

public class MainActivity extends AppCompatActivity implements IFragmentsActivities {
FragmentTransaction fTransaction;
LoginFragment loginFragment;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        loginFragment=new LoginFragment();
        fTransaction=getFragmentManager().beginTransaction();
        fTransaction.add(R.id.fragments_conteiner,loginFragment);
        fTransaction.commit();

    }

    @Override
    public void switchingFragmets(Fragment fragment) {
        fTransaction=getFragmentManager().beginTransaction();
        fTransaction.replace(R.id.fragments_conteiner,fragment);
        fTransaction.addToBackStack(null);
        fTransaction.commit();
    }

    @Override
    public void signIn(User user) {

    }


}
