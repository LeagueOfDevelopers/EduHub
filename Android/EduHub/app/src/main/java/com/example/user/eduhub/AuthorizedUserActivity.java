package com.example.user.eduhub;

import android.content.*;
import android.os.Bundle;
import android.support.annotation.LayoutRes;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.ViewTreeObserver;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;

import com.auth0.android.jwt.JWT;
import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Fakes.FakeGroupRepository;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Fragments.Authorized_fragment;
import com.example.user.eduhub.Fragments.Authorized_fragment2;
import com.example.user.eduhub.Interfaces.View.IGroupListView;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.Retrofit.EduHubApi;

import java.util.ArrayList;

import io.reactivex.disposables.Disposable;

public class AuthorizedUserActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener{

    ViewPager pager;
    ViewPagerAdapter adapter;
    Authorized_fragment authorized_fragment;
    Authorized_fragment authorized_fragment2;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    FakesButton fakesButton=new FakesButton();
    EduHubApi eduHubApi;
    Disposable disposable;
    Boolean bool;
     android.content.SharedPreferences sPref;
    Toolbar toolbar;
    GroupsPresenter groupsPresenter;
    CheckBox checkFakes;
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";



    Button btn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_authorized_user);
        toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");
        setSupportActionBar(toolbar);

        sPref=getSharedPreferences("User",MODE_PRIVATE);
        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(AVATARLINK)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){
             user=savedDataRepository.loadSavedData(sPref);}else{
            Intent intent=getIntent();
            user=(User) intent.getSerializableExtra("user");
            savedDataRepository=new SavedDataRepository();
            SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail());
        }

        bool=savedDataRepository.loadCheckButtonResult(sPref);
        fakesButton.setCheckButton(bool);


        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        Log.d("User name",user.getName());

        navigationView.setNavigationItemSelectedListener(this);
        authorized_fragment=new Authorized_fragment();

        authorized_fragment2=new Authorized_fragment();

        btn=findViewById(R.id.btn);

        pager=findViewById(R.id.pager);
        adapter=new ViewPagerAdapter(getSupportFragmentManager());
        adapter.addFragment(authorized_fragment,"Обучение");
        adapter.addFragment(authorized_fragment2,"Преподавание");

        pager.setAdapter(adapter);

        TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
        tabLayout.setupWithViewPager(pager);


        ViewTreeObserver vto = navigationView.getViewTreeObserver();
        vto.addOnGlobalLayoutListener
                (new ViewTreeObserver.OnGlobalLayoutListener() { @Override public void onGlobalLayout() {
                    TextView textView=findViewById(R.id.name_user);
                    textView.setText(user.getName());
                    checkFakes=findViewById(R.id.checkFakes);
                    checkFakes.setChecked(fakesButton.getCheckButton());
                    checkFakes.setOnClickListener(click->{
                        fakesButton.setCheckButton(checkFakes.isChecked());
                        Log.d("CheckBUtoon" ,fakesButton.getCheckButton().toString());
                        SaveCheckButtonResult(fakesButton.getCheckButton());
                    });


        } });


    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.second_menu, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();



        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.main ) {
            Intent intent=new Intent(this,AuthorizedUserActivity.class);
            startActivity(intent);
        } else if (id == R.id.profile) {

        } else if (id == R.id.myGroups) {

        } else if (id == R.id.becameTeacher) {

        } else if (id == R.id.settings) {

        } else if (id == R.id.notification) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
    @Override
    public void onDestroy() {
        super.onDestroy();
        disposable.dispose();
    }
    private void SaveUser(String token,String name,String avatarLink,String email){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        JWT jwt = new JWT(token);
        editor.putString(ROLE,jwt.getClaim("Role").asString());
        editor.putString(ID,jwt.getClaim("UserId").asString());
        editor.putString(TOKEN,token);
        editor.putString(NAME,name);
        editor.putString(AVATARLINK,avatarLink);
        editor.putString(EMAIL,email);
        editor.commit();
    }
    private void SaveCheckButtonResult(Boolean bool){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        editor.putBoolean("CheckButton",bool);
        editor.commit();
    }


}
