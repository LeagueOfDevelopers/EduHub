package com.example.user.eduhub;

import android.content.*;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.view.ViewPager;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.auth0.android.jwt.JWT;
import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Fakes.FakeAcocuntActivities;
import com.example.user.eduhub.Fragments.Authorized_fragment;
import com.example.user.eduhub.Models.Group.Group;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Retrofit.EduHubApi;
import com.example.user.eduhub.Retrofit.RetrofitBuilder;

import java.util.ArrayList;

import io.reactivex.android.schedulers.AndroidSchedulers;
import io.reactivex.disposables.Disposable;
import io.reactivex.schedulers.Schedulers;

public class AuthorizedUserActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener{

    ViewPager pager;
    ViewPagerAdapter adapter;
    Authorized_fragment authorized_fragment;
    TextView name;
    User user;
    EduHubApi eduHubApi;
    Disposable disposable;
    ArrayList<Group> groups;
     android.content.SharedPreferences sPref;
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";

    FakeAcocuntActivities fakeAcocuntActivities=new FakeAcocuntActivities();

    Button btn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_authorized_user);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        Intent intent=getIntent();
        user=(User) intent.getSerializableExtra("user");
        SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail());
        MakeToast(user.getName());
        eduHubApi= RetrofitBuilder.getApi();
        disposable=eduHubApi.getGroups()
                .subscribeOn(Schedulers.io())//вверх
                .observeOn(AndroidSchedulers.mainThread())//вниз

                .subscribe(
                        next->{
                            groups=next.getGroups();},
                        error->{
                            Log.d("ERROR",error+"");
                            MakeToast("ошибочка вышла");},
                        ()-> {
                            MakeToast("Все прошло успешно");
                            authorized_fragment=new Authorized_fragment();
                            authorized_fragment.setGroups(groups);
                            btn=findViewById(R.id.btn);

                            pager=findViewById(R.id.pager);
                            adapter=new ViewPagerAdapter(getSupportFragmentManager());
                            adapter.addFragment(authorized_fragment,"Обучение");

                            pager.setAdapter(adapter);

                            TabLayout tabLayout = (TabLayout) findViewById(R.id.tabs);
                            tabLayout.setupWithViewPager(pager);
                            DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
                            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                                    this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
                            drawer.addDrawerListener(toggle);
                            toggle.syncState();

                            NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
                            name=(TextView) navigationView.findViewById(R.id.name_user);
                            TextView email=navigationView.findViewById(R.id.email_user);
                            name.setText(user.getName());
                            email.setText(user.getEmail());
                            navigationView.setNavigationItemSelectedListener(this);
                        });
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

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.main ) {
            // Handle the camera action
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

    private void MakeToast(String s){
        Toast toast = Toast.makeText(getApplicationContext(),
                (s), Toast.LENGTH_LONG);
        toast.show();
    }
}
