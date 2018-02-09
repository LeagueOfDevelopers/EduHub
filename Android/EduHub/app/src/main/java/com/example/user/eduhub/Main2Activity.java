package com.example.user.eduhub;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.design.widget.TabLayout;
import android.support.v4.view.GravityCompat;
import android.support.v4.view.MenuItemCompat;
import android.support.v4.view.ViewPager;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.SearchView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.ViewTreeObserver;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;

import com.example.user.eduhub.Adapters.ViewPagerAdapter;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Fragments.TeacherFragment;
import com.example.user.eduhub.Fragments.UserFragment;

public class Main2Activity extends AppCompatActivity implements  NavigationView.OnNavigationItemSelectedListener{
ViewPager pager;
ViewPagerAdapter adapter;
UserFragment userFragment;
TeacherFragment teacherFragment;
SharedPreferences sPref;
Button btn;
CheckBox checkFakes;
FakesButton fakesButton=new FakesButton();
    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        Log.d("pref",sPref.contains(TOKEN)+"");

        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){

            Intent intent=new Intent(this,AuthorizedUserActivity.class);

            startActivity(intent);
        }
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("EduHub");
        setSupportActionBar(toolbar);
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);


        navigationView.setNavigationItemSelectedListener(this);
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
        ViewTreeObserver vto = navigationView.getViewTreeObserver();
        vto.addOnGlobalLayoutListener
                (new ViewTreeObserver.OnGlobalLayoutListener() { @Override public void onGlobalLayout() {
                    TextView textView=findViewById(R.id.name_user);
                    textView.setText("Гость");
                    TextView emailView=findViewById(R.id.email_user);
                    emailView.setText("Гость");
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
        getMenuInflater().inflate(R.menu.second_menu, menu);
        MenuItem searchViewItem = menu.findItem(R.id.action_search);
        final SearchView searchViewAndroidActionBar = (SearchView) MenuItemCompat.getActionView(searchViewItem);
        searchViewAndroidActionBar.setQueryHint("Поиск");
        searchViewAndroidActionBar.setMaxWidth(1000);
        searchViewAndroidActionBar.setOnQueryTextListener(new SearchView.OnQueryTextListener() {
            @Override
            public boolean onQueryTextSubmit(String query) {
                searchViewAndroidActionBar.clearFocus();
                return true;
            }

            @Override
            public boolean onQueryTextChange(String newText) {
                return false;
            }
        });
        return super.onCreateOptionsMenu(menu);
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
            Intent intent = new Intent(this,MainActivity.class);
            startActivity(intent);
        } else if (id == R.id.profile) {
            Intent intent = new Intent(this,MainActivity.class);
            startActivity(intent);
        } else if (id == R.id.myGroups) {
            Intent intent = new Intent(this,MainActivity.class);
            startActivity(intent);
        } else if (id == R.id.becameTeacher) {
            Intent intent = new Intent(this,MainActivity.class);
            startActivity(intent);
        } else if (id == R.id.settings) {
            Intent intent = new Intent(this,MainActivity.class);
            startActivity(intent);
        } else if (id == R.id.notification) {
            Intent intent = new Intent(this,MainActivity.class);
            startActivity(intent);
        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
    private void SaveCheckButtonResult(Boolean bool){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        editor.putBoolean("CheckButton",bool);
        editor.commit();
    }



}
