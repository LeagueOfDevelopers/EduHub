package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.widget.Button;
import android.widget.Toast;

import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Fakes.FakeAcocuntActivities;
import com.example.user.eduhub.Fragments.Chat;
import com.example.user.eduhub.Fragments.CreateGroupFragment;
import com.example.user.eduhub.Fragments.TeacherFragment;
import com.example.user.eduhub.Fragments.UserFragment;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

public class Main2Activity extends AppCompatActivity {
ViewPager pager;
ViewPagerAdapter adapter;
UserFragment userFragment;
TeacherFragment teacherFragment;
CreateGroupFragment createGroupFragment;
EduHubApi eduHubApi;
ArrayList<Group> groups;
SavedDataRepository savedDataRepository=new SavedDataRepository();
SharedPreferences sPref;
Button btn;
Disposable disposable;
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);
        sPref=getSharedPreferences("User",MODE_PRIVATE);

        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(AVATARLINK)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){
            User user=savedDataRepository.loadSavedData(sPref);
            Intent intent=new Intent(this,AuthorizedUserActivity.class);
            intent.putExtra("user",user);
            startActivity(intent);
        }
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        btn=findViewById(R.id.btn);
        teacherFragment=new TeacherFragment();
        userFragment=new UserFragment();
        createGroupFragment=new CreateGroupFragment();
        eduHubApi= RetrofitBuilder.getApi();
       disposable= eduHubApi.getGroups()
                .subscribeOn(Schedulers.io())//вверх
                .observeOn(AndroidSchedulers.mainThread())//вниз

                .subscribe(
                        next->{
                            groups=next.getGroups();},
                        error->{
                            Log.d("ERROR",error+"");
                            MakeToast("ошибочка вышла");},
                        ()->{MakeToast("Все прошло успешно");
                            userFragment.setGroups(groups);
                            teacherFragment.setGroups(groups);
                            pager=findViewById(R.id.pager);
                            adapter=new ViewPagerAdapter(getSupportFragmentManager());
                            adapter.addFragment(userFragment,"Обучение");
                            adapter.addFragment(teacherFragment ,"Преподавание");
                            adapter.addFragment(createGroupFragment ,"test");
                            pager.setAdapter(adapter);

                            TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
                            tabLayout.setupWithViewPager(pager);});
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        disposable.dispose();
    }
    private void MakeToast(String s){
        Toast toast = Toast.makeText(getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }

}
