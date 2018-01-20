package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.widget.Button;

import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Fragments.TeacherFragment;
import com.example.user.eduhub.Fragments.UserFragment;
import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.disposables.Disposable;

public class Main2Activity extends AppCompatActivity {
ViewPager pager;
ViewPagerAdapter adapter;
UserFragment userFragment;
TeacherFragment teacherFragment;
SharedPreferences sPref;
Button btn;
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);
        sPref=getSharedPreferences("User",MODE_PRIVATE);


        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(AVATARLINK)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){

            Intent intent=new Intent(this,AuthorizedUserActivity.class);

            startActivity(intent);
        }
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");
        setSupportActionBar(toolbar);
        btn=findViewById(R.id.btn);
        teacherFragment=new TeacherFragment();
        userFragment=new UserFragment();
        pager=findViewById(R.id.pager);
        adapter=new ViewPagerAdapter(getSupportFragmentManager());
        adapter.addFragment(userFragment,"Обучение");
        adapter.addFragment(teacherFragment ,"Преподавание");
        pager.setAdapter(adapter);

        TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(pager);

    }





}
