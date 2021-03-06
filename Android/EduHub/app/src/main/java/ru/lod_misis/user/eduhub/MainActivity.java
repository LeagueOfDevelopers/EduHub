package ru.lod_misis.user.eduhub;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import com.example.user.eduhub.R;

import ru.lod_misis.user.eduhub.Fragments.LoginFragment;
import ru.lod_misis.user.eduhub.Interfaces.IFragmentsActivities;
import ru.lod_misis.user.eduhub.Models.User;

public class MainActivity extends AppCompatActivity implements IFragmentsActivities {
    FragmentTransaction fTransaction;
    LoginFragment loginFragment;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        loginFragment=new LoginFragment();
        fTransaction=getSupportFragmentManager().beginTransaction();
        fTransaction.add(R.id.fragments_conteiner,loginFragment);
        fTransaction.commit();

    }

    @Override
    public void switchingFragmets(Fragment fragment) {
        fTransaction=getSupportFragmentManager().beginTransaction();
        fTransaction.replace(R.id.fragments_conteiner,fragment);
        fTransaction.commit();
    }

    @Override
    public void signIn(User user) {

    }


}
