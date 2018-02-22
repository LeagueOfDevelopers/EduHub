package com.example.user.eduhub;

import android.content.*;
import android.os.Bundle;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.MenuItemCompat;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
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

import com.auth0.android.jwt.JWT;
import com.example.user.eduhub.Fakes.FakesButton;
import com.example.user.eduhub.Fragments.Authorized_fragment;
import com.example.user.eduhub.Fragments.MainFragment;
import com.example.user.eduhub.Fragments.NotificationFragment;
import com.example.user.eduhub.Fragments.ProfileTransactionFragment;
import com.example.user.eduhub.Fragments.UserProfileFragment;
import com.example.user.eduhub.Fragments.UsersGroupsFragment;
import com.example.user.eduhub.Models.SavedDataRepository;
import com.example.user.eduhub.Models.User;
import com.example.user.eduhub.Presenters.GroupsPresenter;
import com.example.user.eduhub.Retrofit.EduHubApi;

import io.reactivex.disposables.Disposable;

public class AuthorizedUserActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener{

    FragmentTransaction fragmentTransaction;
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
        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){
             user=savedDataRepository.loadSavedData(sPref);
            bool=savedDataRepository.loadCheckButtonResult(sPref);
            fakesButton.setCheckButton(bool);}
             else{
            Intent intent=getIntent();
            user=(User) intent.getSerializableExtra("user");
            savedDataRepository=new SavedDataRepository();
            SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail());

        }





        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        Log.d("User name",user.getName());

        navigationView.setNavigationItemSelectedListener(this);
        MainFragment mainFragment=new MainFragment();
        fragmentTransaction=getSupportFragmentManager().beginTransaction();
        fragmentTransaction.add(R.id.main_fragments_conteiner,mainFragment);
        fragmentTransaction.commit();




        ViewTreeObserver vto = navigationView.getViewTreeObserver();
        vto.addOnGlobalLayoutListener
                (new ViewTreeObserver.OnGlobalLayoutListener() { @Override public void onGlobalLayout() {
                    TextView textView=findViewById(R.id.name_user);
                    textView.setText(user.getName());
                    TextView textView1=findViewById(R.id.email_user);
                    textView1.setText(user.getEmail());
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
            Intent startMain = new Intent(Intent.ACTION_MAIN);
            startMain.addCategory(Intent.CATEGORY_HOME);
            startMain.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK);
            startActivity(startMain);
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
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
            MainFragment mainFragment=new MainFragment();
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
            fragmentTransaction.add(R.id.main_fragments_conteiner,mainFragment);
            fragmentTransaction.commit();
        } else if (id == R.id.profile) {
            ProfileTransactionFragment profileTransactionFragment=new ProfileTransactionFragment();
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
            fragmentTransaction.replace(R.id.main_fragments_conteiner,profileTransactionFragment);

            fragmentTransaction.commit();
        } else if (id == R.id.myGroups) {
            UsersGroupsFragment usersGroupsFragment=new UsersGroupsFragment();
            usersGroupsFragment.setToken(user);
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
            fragmentTransaction.replace(R.id.main_fragments_conteiner,usersGroupsFragment);

            fragmentTransaction.commit();
        } else if (id == R.id.settings) {

        } else if (id == R.id.notification) {
            NotificationFragment notificationFragment=new NotificationFragment();
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
            fragmentTransaction.replace(R.id.main_fragments_conteiner,notificationFragment);

            fragmentTransaction.commit();

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    private void SaveUser(String token,String name,String avatarLink,String email){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        JWT jwt = new JWT(token);
        editor.putString(ROLE,jwt.getClaim("Role").asString());
        editor.putString(ID,jwt.getClaim("UserId").asString());
        editor.putString(TOKEN,token);
        editor.putString(NAME,name);
        editor.putString(EMAIL,email);
        editor.commit();
    }
    private void SaveCheckButtonResult(Boolean bool){
        android.content.SharedPreferences.Editor editor=sPref.edit();
        editor.putBoolean("CheckButton",bool);
        editor.commit();
    }



}
