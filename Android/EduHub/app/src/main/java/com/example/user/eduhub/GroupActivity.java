package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.ImageButton;
import android.widget.TextView;

import com.example.user.eduhub.Fragments.MainGroupFragment;
import com.example.user.eduhub.Interfaces.IFragmentsActivities;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;

public class GroupActivity extends AppCompatActivity
        implements  IFragmentsActivities{
    Group group;
    FragmentTransaction transaction;
    MainGroupFragment mainGroupFragment;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;TextView name,email;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_group);
        Intent intent=getIntent();
        group=(Group) intent.getSerializableExtra("group") ;
        ImageButton back=findViewById(R.id.back);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");
        setSupportActionBar(toolbar);
        SharedPreferences sPref=sPref=getSharedPreferences("User",MODE_PRIVATE);
        user= savedDataRepository.loadSavedData(sPref);
        Log.d("Check_User",user.getUserId());
        mainGroupFragment=new MainGroupFragment();
        mainGroupFragment.setGroup(group);
        mainGroupFragment.setUser(user);
        transaction=getSupportFragmentManager().beginTransaction();
        transaction.add(R.id.group_fragments_conteiner,mainGroupFragment);
        transaction.commit();
        back.setOnClickListener(click->{
            Intent intent1 = new Intent(this,AuthorizedUserActivity.class);

            startActivity(intent1);
        });

    }






    @Override
    public void switchingFragmets(Fragment fragment) {
        transaction=getSupportFragmentManager().beginTransaction();
        transaction.replace(R.id.group_fragments_conteiner,fragment);
        transaction.addToBackStack(null);
        transaction.commit();
    }

    @Override
    public void signIn(User user) {

    }
}
