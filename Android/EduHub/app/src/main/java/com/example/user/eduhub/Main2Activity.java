package com.example.user.eduhub;

import android.app.Activity;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.support.design.widget.NavigationView;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;

import com.example.user.eduhub.Adapters.GroupAdapter;
import com.example.user.eduhub.Classes.Group;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.GregorianCalendar;

public class Main2Activity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {
RecyclerView recyclerView;
    RecyclerView recyclerView2;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
recyclerView=findViewById(R.id.recycler1);
        recyclerView2=findViewById(R.id.recycler2);
        recyclerView.setHasFixedSize(true);
        recyclerView2.setHasFixedSize(true);
        LinearLayoutManager llm = new LinearLayoutManager(getApplicationContext(),LinearLayoutManager.HORIZONTAL,false);
        LinearLayoutManager llm2 = new LinearLayoutManager(getApplicationContext(),LinearLayoutManager.HORIZONTAL,false);
        recyclerView.setLayoutManager(llm);
        recyclerView2.setLayoutManager(llm2);
        GroupAdapter adapter = new GroupAdapter(getFakeGroups());
        recyclerView.setAdapter(adapter);
        recyclerView2.setAdapter(adapter);




        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
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
        getMenuInflater().inflate(R.menu.main2, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement


        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

        if (id == R.id.nav_camera) {
            // Handle the camera action
        } else if (id == R.id.nav_gallery) {

        } else if (id == R.id.nav_slideshow) {

        } else if (id == R.id.nav_manage) {

        } else if (id == R.id.nav_share) {

        } else if (id == R.id.nav_send) {

        }

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }
    private ArrayList<Group> getFakeGroups(){
        ArrayList<Group> groups=new ArrayList<>();
        GregorianCalendar gregorianCalendar=new GregorianCalendar(2017, Calendar.DECEMBER,13,21,00);

        groups.add(new Group("C# easy",8,3,new ArrayList<String>(),gregorianCalendar.getTime() ));
        groups.add(new Group("Java для чайников",8,5,new ArrayList<String>(),new GregorianCalendar(2017,Calendar.DECEMBER,31).getTime()));
        return groups;
    }
}
