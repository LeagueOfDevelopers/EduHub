package ru.lod_misis.user.eduhub.Fragments;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;

import ru.lod_misis.user.eduhub.Adapters.ViewPagerAdapter;
import ru.lod_misis.user.eduhub.Interfaces.IFragmentsActivities;
import ru.lod_misis.user.eduhub.InviteUserToGroup;
import ru.lod_misis.user.eduhub.Models.Group.Group;
import ru.lod_misis.user.eduhub.Models.SavedDataRepository;
import ru.lod_misis.user.eduhub.Models.User;
import com.example.user.eduhub.R;

import static android.content.Context.MODE_PRIVATE;

/**
 * Created by User on 12.01.2018.
 */

public class MainGroupFragment extends Fragment {
    ViewPager pager;
    ViewPagerAdapter adapter;
    TabLayout tabLayout;
    ChatFragment chat;
    IFragmentsActivities fragmentsActivities;
    GroupMembersFragment aboutGroupFragment;
    GroupInformationFragment groupInformationFragment;
    Group group;
    User user;
    SavedDataRepository savedDataRepository=new SavedDataRepository();
    Toolbar toolbar;

    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            fragmentsActivities = (IFragmentsActivities) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString() + " must implement onSomeEventListener");
        }
    }

    public void setAboutGroupFragment(GroupMembersFragment aboutGroupFragment) {
        this.aboutGroupFragment = aboutGroupFragment;
    }

    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, Bundle savedInstanceState) {
        View v = inflater.inflate(R.layout.main_group_fragment, null);
        Log.d("Group",group.toString());
         toolbar=getActivity().findViewById(R.id.toolbar);
        toolbar.setTitle(group.getGroupInfo().getTitle());


        SharedPreferences sPref=getActivity().getSharedPreferences("User",MODE_PRIVATE);
        user= savedDataRepository.loadSavedData(sPref);
        pager=v.findViewById(R.id.pager);
        tabLayout = (TabLayout) v.findViewById(R.id.tabs);

        if(adapter==null){

        chat=new ChatFragment();
        chat.setGroup(group);
        chat.setUser(user);


        aboutGroupFragment.setGroup(group);
        aboutGroupFragment.setUser(user);

        adapter=new ViewPagerAdapter(getFragmentManager());
        adapter.addFragment(aboutGroupFragment,"Информация");
        adapter.addFragment(chat,"Чат");
        }

        pager.setAdapter(adapter);
        tabLayout.setTabTextColors(Color.WHITE,Color.WHITE);
        tabLayout.setupWithViewPager(pager);







        return v;
    }

    public void setAdapter(ViewPagerAdapter adapter) {
        this.adapter = adapter;
    }

    @Override
    public void onResume() {
        super.onResume();

    }

    public void setGroup(Group group){
        this.group=group;
    }





}
