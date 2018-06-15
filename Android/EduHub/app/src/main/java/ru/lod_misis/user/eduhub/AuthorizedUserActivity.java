package ru.lod_misis.user.eduhub;

import android.content.*;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
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
import android.view.View;
import android.view.ViewTreeObserver;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.Filter;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.TextView;

import com.auth0.android.jwt.JWT;
import com.example.user.eduhub.R;
import com.jakewharton.retrofit2.adapter.rxjava2.HttpException;

import ru.lod_misis.user.eduhub.Classes.FiltresModel;
import ru.lod_misis.user.eduhub.Fakes.FakesButton;
import ru.lod_misis.user.eduhub.Fragments.CommonFragmentForNotifications;
import ru.lod_misis.user.eduhub.Fragments.FindGroupsFragment;
import ru.lod_misis.user.eduhub.Fragments.MainFragment;
import ru.lod_misis.user.eduhub.Fragments.InvitationFragment;
import ru.lod_misis.user.eduhub.Fragments.NotificationSettings;
import ru.lod_misis.user.eduhub.Fragments.ProfileFragment;
import ru.lod_misis.user.eduhub.Fragments.UsersGroupsFragment;
import ru.lod_misis.user.eduhub.Interfaces.View.IFileRepositoryView;
import ru.lod_misis.user.eduhub.Interfaces.View.IFindGroupsView;
import ru.lod_misis.user.eduhub.Interfaces.View.IRefreshTokenView;
import ru.lod_misis.user.eduhub.Models.AddFileResponseModel;
import ru.lod_misis.user.eduhub.Models.DecodeFile;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import ru.lod_misis.user.eduhub.Presenters.FIndGroupsPresenter;
import ru.lod_misis.user.eduhub.Presenters.FileRepository;
import ru.lod_misis.user.eduhub.Presenters.GroupsPresenter;
import ru.lod_misis.user.eduhub.Presenters.RefreshTokenPresenter;
import ru.lod_misis.user.eduhub.Retrofit.EduHubApi;

import java.io.IOException;
import java.util.ArrayList;

import io.reactivex.disposables.Disposable;
import okhttp3.ResponseBody;

public class AuthorizedUserActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener,IRefreshTokenView,IFileRepositoryView,IFindGroupsView {

    FragmentTransaction fragmentTransaction;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    User user;
    ImageView imageView;
    Bitmap bitmap;
    FakesButton fakesButton=new FakesButton();
    EduHubApi eduHubApi;
    Disposable disposable;
    Context context=this;
    Boolean bool;
     android.content.SharedPreferences sPref;
    Toolbar toolbar;
    GroupsPresenter groupsPresenter;
    CheckBox checkFakes;

    final  String TOKEN="TOKEN",NAME="NAME",AVATARLINK="AVATARLINK",EMAIL="EMAIL",ID="ID",ROLE="ROLE",EXP="EXP";
    RefreshTokenPresenter refreshTokenPresenter=new RefreshTokenPresenter(this);

    FileRepository fileRepository;
    DecodeFile decodeFile=new DecodeFile(this);
    FIndGroupsPresenter fIndGroupsPresenter=new FIndGroupsPresenter(this);
    FiltresModel filtresModel;


    Button btn;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_authorized_user);
        toolbar = (Toolbar) findViewById(R.id.toolbar);
        toolbar.setTitle("");

        setSupportActionBar(toolbar);
        Log.d("Cont",context.toString());
        filtresModel=FiltresModel.getInstance();
        filtresModel.setType("Default");
        fileRepository=new FileRepository(this,context );
        sPref=getSharedPreferences("User",MODE_PRIVATE);
        if(sPref.contains(TOKEN)&&sPref.contains(NAME)&&sPref.contains(EMAIL)&&sPref.contains(ID)&&sPref.contains(ROLE)){
           Integer exp=savedDataRepository.loadExp(sPref);
            user=savedDataRepository.loadSavedData(sPref);
            bool=savedDataRepository.loadCheckButtonResult(sPref);
            fakesButton.setCheckButton(bool);

            refreshTokenPresenter.refreshToken(savedDataRepository.loadSavedData(sPref).getToken(),this);
            if(user.getAvatarLink()!=null){


                fileRepository.loadImageFromServer(user.getToken(),user.getAvatarLink());

            }
        }
             else{
            Intent intent=getIntent();
            user=(User) intent.getSerializableExtra("user");
            savedDataRepository=new SavedDataRepository();
            filtresModel=FiltresModel.getInstance();
            JWT jwt = new JWT(user.getToken());
            user.setUserId(jwt.getClaim("UserId").asString());
            user.setRole(jwt.getClaim("Role").asString());
            savedDataRepository.SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail(),user.getTeacher(),sPref);
            drawer();

        }








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
            public boolean onQueryTextChange(String newText)
            {
                if(newText.equals("")){
                    MainFragment mainFragment=new MainFragment();
                    fragmentTransaction=getSupportFragmentManager().beginTransaction();
                    fragmentTransaction.add(R.id.main_fragments_conteiner,mainFragment);
                    fragmentTransaction.commit();
                    return true;
                }else{
                if(fakesButton.getCheckButton()){
                    return false;
                }else{
                    Log.d("Поиск...","Поиск");
                    filtresModel.setTittle(newText);
                    fIndGroupsPresenter.findGroupsWithFilters(filtresModel.getMinCost(),filtresModel.getMaxCost(),filtresModel.getTittle(),filtresModel.getTags(),filtresModel.getType(),filtresModel.getPrivacy(),context);
                    return true;}
                }
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
            fragmentTransaction.replace(R.id.main_fragments_conteiner,mainFragment);
            fragmentTransaction.commit();
        } else if (id == R.id.profile) {
            ProfileFragment profileTransactionFragment=new ProfileFragment();
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
            NotificationSettings notificationSettings=new NotificationSettings();
            Log.d("Rolrlrlrlrlr",user.getRole());
            notificationSettings.setUser(user);
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
            fragmentTransaction.replace(R.id.main_fragments_conteiner,notificationSettings);

            fragmentTransaction.commit();
        } else if (id == R.id.notification) {
             CommonFragmentForNotifications commonFragmentForNotifications=new CommonFragmentForNotifications();
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
            fragmentTransaction.replace(R.id.main_fragments_conteiner,commonFragmentForNotifications);

            fragmentTransaction.commit();

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }




    @Override
    public void showLoading() {


    }

    @Override
    public void stopLoading() {
    }

    @Override
    public void getError(Throwable error) {
        if(error instanceof HttpException){
            switch (((HttpException) error).code()){
                case 401:{Intent intent=new Intent(this, Main2Activity.class);
                    SharedPreferences.Editor editor=sPref.edit();
                    editor.clear();
                    editor.commit();
                    startActivity(intent);}
            }
        }

    }

    @Override
    public void getResponse(User user) {

        JWT jwt = new JWT(user.getToken());
        user.setUserId(jwt.getClaim("UserId").asString());
        Log.d("USERID",user.getUserId());
        savedDataRepository=new SavedDataRepository();
        savedDataRepository.SaveUser(user.getToken(),user.getName(),user.getAvatarLink(),user.getEmail(),user.getTeacher(),sPref);
        this.user=savedDataRepository.loadSavedData(sPref);
        drawer();
    }

    @Override
    public void getThrowable(Throwable error) {
        if(error instanceof HttpException){
            switch (((HttpException) error).code()){
                case 401:{Intent intent=new Intent(this, Main2Activity.class);
                    SharedPreferences.Editor editor=sPref.edit();
                    editor.clear();
                    editor.commit();
                    startActivity(intent);}
            }
        }
    }

    private void drawer(){
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

                    if(bitmap!=null){
                        imageView=findViewById(R.id.imageViewAvatar);
                        imageView.setImageBitmap(bitmap);
                    }
                    TextView textView=findViewById(R.id.name_user);
                    textView.setText(user.getName());
                    TextView textView1=findViewById(R.id.email_user);
                    textView1.setText(user.getEmail());
                    checkFakes=findViewById(R.id.checkFakes);
                    checkFakes.setChecked(fakesButton.getCheckButton());
                    checkFakes.setOnClickListener(click->{
                        fakesButton.setCheckButton(checkFakes.isChecked());
                        Log.d("CheckBUtoon" ,fakesButton.getCheckButton().toString());
                        savedDataRepository.SaveCheckButtonResult(fakesButton.getCheckButton(),sPref);
                    });


                } });
    }

    @Override
    public void getResponse(AddFileResponseModel addFileResponseModel) {

    }

    @Override
    public void getFile(ResponseBody file) {

        byte[] rawBitmap;
        try {

            rawBitmap=file.bytes();
            Log.d("Проверяемчтозахрень тут",rawBitmap[5]+"");
            bitmap = BitmapFactory.decodeByteArray(rawBitmap,0,rawBitmap.length);



        } catch (IOException e) {
            e.printStackTrace();
        }

    }

    @Override
    public void getGroups(ArrayList<Group> groups) {

        FindGroupsFragment findGroupsFragment=new FindGroupsFragment();
        Bundle bundle=new Bundle();
        bundle.putSerializable("findGroups",groups);
        bundle.putSerializable("filters",filtresModel);
        findGroupsFragment.setArguments(bundle);
            fragmentTransaction=getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.main_fragments_conteiner,findGroupsFragment);
        fragmentTransaction.commit();
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        fragmentTransaction=getSupportFragmentManager().beginTransaction();
        getFragmentManager().beginTransaction().remove(getFragmentManager().findFragmentById(R.id.main_fragments_conteiner)).commit();
        fragmentTransaction.commit();
    }

    @Override
    public void onDetachedFromWindow() {
        super.onDetachedFromWindow();

    }
}
